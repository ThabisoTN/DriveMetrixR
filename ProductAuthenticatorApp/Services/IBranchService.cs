using Microsoft.AspNetCore.Mvc.Rendering;
using ProductAuthenticatorApp.Data;

namespace ProductAuthenticatorApp.Services
{
    public interface IBranchService
    {
        Task<List<SelectListItem>> GetBranchSelectList();
        Task<List<Lease>> GetLeasesByBranch(int branchId);
        

    }
}
