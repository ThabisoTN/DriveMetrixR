using Microsoft.AspNetCore.Mvc.Rendering;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAuthenticatorApp.Services
{
    public interface IProductService
    {
        Task<List<SelectListItem>> GetCategories();
        Task<Product> AddProduct(Product product, string userId);

        Task<List<Product>> GetUserProducts(string userId);
    }
}