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


        //Get Method For Add Product
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            try
            {
                var getCategories= await productService.GetCategories();

                ViewBag.Categories = getCategories;

                return View(getCategories);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw new Exception();
            }

        }


        //Post Method To Add Product To Database
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product, IFormFile imageFile)
        {
            try
            {
                var userId = userManager.GetUserId(User);

                if (!ModelState.IsValid)
                {
                    logger.LogInformation("Invalid data passed");
                    return BadRequest();

                }

                if (userId != null)
                {
                    logger.LogInformation("About To Add product To database...");

                    using var memoryStream = new MemoryStream();
                    await imageFile.CopyToAsync(memoryStream);
                    product.ProductImage = memoryStream.ToArray();


                    await productService.AddProduct(product, userId);

                    logger.LogInformation("Product added successfully.");
                    return View("ViewItems","User");
                }

                logger.LogInformation("user Id Not valid");
                return BadRequest("Invalid User Id");

            }
            catch (Exception ex)
            {
                logger.LogError($"Error Occured When Trying To add product to Database : {ex.Message}");
                return BadRequest();
            }
        }


        public IActionResult ViewItems() 
        {
            return View();
        }
    }
}
