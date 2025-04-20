using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAuthenticatorApp.Services
{
    public interface IProductService
    {
        Task<List<ProductCategory>> GetCategories();
        
        
    }
}