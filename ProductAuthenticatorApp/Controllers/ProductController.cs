using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NuGet.Protocol.Plugins;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Service;
using ProductAuthenticatorApp.Services;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ProductAuthenticatorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProductService productService;
        private readonly ILogger<ProductController> logger;


        public ProductController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IProductService productService, ILogger<ProductController> logger)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.productService = productService;
            this.logger = logger;
        }

        //GetUserId
        [HttpGet("GetUserId")]
        public async Task<IActionResult> GetUserId()
        {
            try
            {
                var getUserId = userManager.GetUserId(User);
                if (getUserId != null)
                {
                    return Ok(new { getUserId, Message = "UserId Found", success = true });
                }
                
                return BadRequest();
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                throw new Exception();
            }
        }

        //get Product categories
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await productService.GetCategories();

                if (categories != null)
                {
                    logger.LogInformation("Categories Found");
                    return Ok(categories);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }


        //Add product method
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] Product product)
        {
            try
            {
                var userId = userManager.GetUserId(User);

                if (!ModelState.IsValid)
                {
                    logger.LogInformation("Invalid data passed");
                    return BadRequest();

                }

                if (product == null)
                {
                    logger.LogInformation("Product data Not Passed!!!");
                    return BadRequest("Product data is required.");
                }

                if (userId!=null)
                {
                    logger.LogInformation("About To Add product To database...");
                    await productService.AddProduct(product, userId);
                    logger.LogInformation("Product added successfully.");
                    return Ok(new { Message="product added Successfully", success=true, product});
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
    }
}
