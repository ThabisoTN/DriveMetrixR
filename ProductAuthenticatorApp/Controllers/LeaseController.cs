using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Services;
using System;
using System.Threading.Tasks;

namespace ProductAuthenticatorApp.Controllers
{
    [Authorize]
    public class LeaseController : Controller
    {
        private readonly ILeaseService _leaseService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext dbContext;

        public LeaseController(
            ILeaseService leaseService,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext)
        {
            _leaseService = leaseService;
            _userManager = userManager;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Create(int vehicleId)
        {
            // Verify vehicle exists
            var vehicle =dbContext.Vehicles.Find(vehicleId);
            if (vehicle == null)
            {
                return NotFound();
            }

            ViewBag.VehicleId = vehicleId;
            ViewBag.VehicleInfo = $"{vehicle.Make} {vehicle.Model} ({vehicle.Year})";
            Console.WriteLine($"Creating lease request for vehicle: {ViewBag.VehicleInfo}");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            int vehicleId,
            string driverName,
            string driverLicenseNumber,
            string driverEmail,
            string driverPhone,
            string driverAddress,
            DateTime? driverDateOfBirth,
            DateTime startDate,
            DateTime? endDate,
            string terms)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var lease = await _leaseService.CreateLeaseRequest(
                    vehicleId,
                    currentUser.Id,
                    driverName,
                    driverLicenseNumber,
                    driverEmail,
                    driverPhone,
                    driverAddress,
                    driverDateOfBirth,
                    startDate,
                    endDate,
                    terms);

                TempData["SuccessMessage"] = "Lease request submitted successfully!";
                return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while processing your request");
                return View();
            }
        }
    }
}