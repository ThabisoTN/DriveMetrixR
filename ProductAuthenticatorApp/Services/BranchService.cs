using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Service;

namespace ProductAuthenticatorApp.Services
{
    public class BranchService:IBranchService
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<AdminService> logger;

        public BranchService(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, ILogger<AdminService> logger)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.logger = logger;
        }

        // Get All Branches List
        public async Task<List<SelectListItem>> GetBranchSelectList()
        {
            try
            {
                return await dbContext.Branches
                    .Select(b => new SelectListItem
                    {
                        Value = b.BranchId.ToString(),
                        Text = b.Name
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting branch select list: {ex.Message}");
                return new List<SelectListItem>();
            }
        }


        //Get Requests
        public async Task<List<Lease>> GetLeasesByBranch(int branchId)
        {
            return await dbContext.Leases
                .Include(l => l.Vehicle)
                .Include(l => l.Client)
                    .ThenInclude(c => c.ApplicationUser)
                .Include(l => l.Driver)
                .Where(l => l.BranchId == branchId)
                .OrderByDescending(l => l.RequestDate)
                .ToListAsync();
        }
    }
}
