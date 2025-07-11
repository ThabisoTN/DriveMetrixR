using Microsoft.AspNetCore.Mvc.Rendering;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAuthenticatorApp.Services
{
    public interface IProductService
    {
        Task<List<Supplier>> GetSuppliers();
        Task<List<Branch>> GetBranches();
   Task<List<ApplicationUser>> GetApplicationUsers();
        
    }
}