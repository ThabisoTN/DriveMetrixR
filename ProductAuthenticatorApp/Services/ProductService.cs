using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Services;
using System.Security.Claims;

namespace ProductAuthenticatorApp.Service
{
    public class ProductService: IProductService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<ProductService> logger;

        public ProductService(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, ILogger<ProductService> logger)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.logger = logger;
        }

        //Get Categories
        public async Task<List<SelectListItem>> GetCategories()
        {
            try
            {
                var getCategories = await dbContext.ProductCategories
                    .Select(pc => new SelectListItem
                    {
                        Value = pc.Id.ToString(),
                        Text = pc.CategoryName,
                    })
                    .ToListAsync();

                return getCategories;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                throw;
            }
        }


        //Add Product Method
        public async Task<Product> AddProduct(Product product, string userId)
        {
            try
            {
                var newProduct = new Product
                {
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    SerialNumber = product.SerialNumber,
                    MACAddress = product.MACAddress,
                    ModelNumber = product.ModelNumber,
                    Manufacturer = product.Manufacturer,
                    Color = product.Color,
                    StorageCapacity = product.StorageCapacity,
                    RAM = product.RAM,
                    ProcessorModel = product.ProcessorModel,
                    OperatingSystem = product.OperatingSystem,
                    CategoryId =product.CategoryId,
                    ProductImage=product.ProductImage,
                    UserId=userId
                };
                    await dbContext.Products.AddAsync(newProduct);
                    await dbContext.SaveChangesAsync();

                return newProduct;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                throw;
            }
        }


        //Get User Products
        public async Task<List<Product>> GetUserProducts(string userId)
        {
            try
            {
                var getUserProducts = await dbContext.Products
                    .Where(p => p.UserId == userId)
                    .ToListAsync();

                return getUserProducts;

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}

