using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Services;

namespace ProductAuthenticatorApp.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductService _productService;
        private readonly IVehicleService vehicleService;

        public VehicleController(ApplicationDbContext dbContext,UserManager<ApplicationUser> userManager,IProductService productService, IVehicleService vehicleService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _productService = productService;
            this.vehicleService = vehicleService;
        }
        public IActionResult Index()
        {
            return View();
        }

        //View all vehicles
        public async Task<IActionResult> ViewVehicle(int vehicleId)
        {
            try
            {
                var vehicle = await vehicleService.GetVehicleById(vehicleId);
                if (vehicle == null)
                {
                    return NotFound();
                }

                
                vehicle = await _dbContext.Vehicles
                    .Include(v => v.Branch)  
                    .FirstOrDefaultAsync(v => v.VehicleId == vehicleId);

                return View(vehicle);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error viewing vehicle: {ex.Message}");
                return StatusCode(500);
            }
        }
    }
}
