using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Models;
using ProductAuthenticatorApp.Services;
using System.Diagnostics;

namespace ProductAuthenticatorApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductService _productService;
        private readonly IVehicleService vehicleService;

        public ClientController(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            IProductService productService, IVehicleService vehicleService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _productService = productService;
            this.vehicleService = vehicleService;
        }


        //Dashbourd  Get Method
        public IActionResult Index()
        {
            try
            {
                var getVehicles = vehicleService.GetAllVehicles().Result;
                return View(getVehicles);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving vehicles: {ex.Message}");
                return View("Error", new { message = "Unable to retrieve vehicles." });
            }
        }



        //View Leases
        public IActionResult ViewLeases()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var leases = _dbContext.Leases
                    .Include(l => l.Vehicle)
                    .ThenInclude(v => v.Branch)
                    .Include(l => l.Driver)
                    .Where(l => l.ClientUserId == userId)
                    .OrderByDescending(l => l.RequestDate)
                    .ToList();

                ViewBag.leases = leases;

                return View(leases);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error retrieving leases");
                throw new Exception("Unable to retrieve leases", ex);

            }
        }
    }
}
