using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Services;

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
    }
}
