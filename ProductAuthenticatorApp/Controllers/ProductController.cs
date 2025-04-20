using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Services;

namespace ProductAuthenticatorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> usermanager;
       
        public ProductController(ApplicationDbContext dbContext, UserManager<ApplicationUser> usermanager)
        {
            this.dbContext = dbContext;
            this.usermanager = usermanager;
        }
        //Get Current userId
        [HttpGet("GetUserId")]
        public async Task<IActionResult> GetUserId()
        {
            try
            {
                var userId = usermanager.GetUserId(User);

                if (userId != null)
                {
                    return Ok(userId);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured When trying To get Current Logged In UserId {ex.Message}");
                return BadRequest(ex.Message );
            }
        }

        //Add product method
        [HttpPost("AddProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>AddProduct([FromBody]Product product)
        {
            try
            {
                Console.WriteLine("About Ton Add product To database...");
                var userId = usermanager.GetUserId(User);

                if (userId!=null)
                {
                    var newProduct = new Product
                    {
                        ProductName = product.ProductName,
                        ProductDescription = product.ProductDescription,
                        SerialNumber = product.SerialNumber,
                        UserId = userId,                        
                    };

                    await dbContext.Products.AddAsync(newProduct);
                    await dbContext.SaveChangesAsync();

                    Console.WriteLine($"{newProduct.ProductName} Added To database");
                    return Ok(newProduct);
                }

                Console.WriteLine("user Id Not valid");
                return BadRequest("Invalid User Id");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured When Trying To add product to Database : {ex.Message}");
                return BadRequest();
            }
        }


       
    }
}
