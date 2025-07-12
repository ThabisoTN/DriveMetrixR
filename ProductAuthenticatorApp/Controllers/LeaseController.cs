using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Services;

[Authorize]
public class LeaseController : Controller
{
    private readonly ILeaseService leaseService;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ApplicationDbContext _context;

    public LeaseController(
        ILeaseService leaseService,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext _context)
    {
        this.leaseService = leaseService;
        this.userManager = userManager;
        this._context = _context;
    }

    [HttpGet]
    public IActionResult Create(int vehicleId)
    {
        var vehicle = _context.Vehicles
            .Include(v => v.Branch)
            .FirstOrDefault(v => v.VehicleId == vehicleId);

        if (vehicle == null)
            return NotFound();

        ViewBag.VehicleId = vehicleId;
        ViewBag.VehicleInfo = $"{vehicle.Make} {vehicle.Model} ({vehicle.Year})";
        ViewBag.BranchId = vehicle.BranchId;
        ViewBag.BranchName = vehicle.Branch?.Name ?? "N/A";
        ViewBag.MonthlyRate = vehicle.LeasingPrice;

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Microsoft.AspNetCore.Http.IFormCollection form)
    {
        try
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var client = await _context.Clients.FirstOrDefaultAsync(c => c.ApplicationUserId == userId);
            if (client == null)
                return BadRequest("Client account not found");

            var vehicleId = int.Parse(form["VehicleId"]);
            var vehicle = await _context.Vehicles.Include(v => v.Branch).FirstOrDefaultAsync(v => v.VehicleId == vehicleId);
            if (vehicle == null || vehicle.Branch == null)
                return NotFound("Vehicle or branch not found");

            using var transaction = await _context.Database.BeginTransactionAsync();

            // 1. Create Driver
            var driver = new Driver
            {
                Name = form["Driver.Name"],
                LicenseNumber = form["Driver.LicenseNumber"],
                Email = form["Driver.Email"],
                Phone = form["Driver.Phone"],
                Address = form["Driver.Address"],
                DateOfBirth = string.IsNullOrEmpty(form["Driver.DateOfBirth"]) ? null : DateTime.Parse(form["Driver.DateOfBirth"])
            };

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            // 2. Create Lease
            var lease = new Lease
            {
                StartDate = DateTime.Parse(form["StartDate"]),
                EndDate = string.IsNullOrEmpty(form["EndDate"]) ? (DateTime?)null : DateTime.Parse(form["EndDate"]),
                Terms = form["Terms"],
                VehicleId = vehicleId,
                BranchId = vehicle.BranchId,  // ✅ This requires column to exist
                ClientId = client.ClientId,
                DriverId = driver.DriverId,
                MonthlyRate = vehicle.LeasingPrice,
                ClientUserId= userId,
                LeaseStatus = "RequestSent",
                RequestDate = DateTime.Now,
                IsActive = true
            };

            _context.Leases.Add(lease);
            await _context.SaveChangesAsync();

            // 3. Mark vehicle as unavailable
            vehicle.IsAvailable = false;
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return RedirectToAction("Index", "Client");
        }
        catch (Exception ex)
        {
            var inner = ex.InnerException?.Message ?? ex.Message;
            ModelState.AddModelError("", $"An error occurred: {inner}");
            //await RepopulateViewData(int.Parse(form["VehicleId"]));
            return View();
        }

    }
}
