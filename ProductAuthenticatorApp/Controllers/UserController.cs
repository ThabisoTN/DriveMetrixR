using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Service;
using ProductAuthenticatorApp.Services;
using System.Threading.Tasks;

namespace ProductAuthenticatorApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<UserController> logger;
        private readonly IProductService productService;

        public UserController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, ILogger<UserController> logger, IProductService productService)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.logger = logger;
            this.productService = productService;
        }


        public IActionResult Index()
        {
            return View();
        }


 


        public IActionResult ViewItems() 
        {
            return View();
        }
    }
}
