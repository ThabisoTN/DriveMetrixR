using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Models;
using ProductAuthenticatorApp.Services;
using System.Diagnostics;

namespace ProductAuthenticatorApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductService _productService;
        private readonly IVehicleService vehicleService;

        public HomeController(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            IProductService productService, IVehicleService vehicleService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _productService = productService;
            this.vehicleService = vehicleService;
        }


        public async Task<IActionResult> Index()
        {
            var vehicles = await vehicleService.GetAllVehicles();
            return View(vehicles);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
