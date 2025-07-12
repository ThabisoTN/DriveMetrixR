using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Services;

namespace ProductAuthenticatorApp.Controllers
{
    [Authorize(Roles = "BranchManager")] // Add authorization
    public class BranchController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBranchService _branchService;

        // Only inject what you need
        public BranchController(ApplicationDbContext dbContext,UserManager<ApplicationUser> userManager, IBranchService branchService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _branchService = branchService;
        }
        public Task<IActionResult> Index()
        {
            return Task.FromResult<IActionResult>(View());
        }


        //[HttpGet]
        //public async Task<IActionResult> ViewBranchLeases()
        //{
        //    var currentUserId = _userManager.GetUserId(User);

        //    var branchManager = await _dbContext.BranchManagers
        //        .Include(bm => bm.Branch)
        //        .FirstOrDefaultAsync(bm => bm.ApplicationuserId == currentUserId);

        //    if (branchManager == null)
        //    {
        //        return Forbid(); 
        //    }

        //    var leases = await _branchService.GetLeasesByBranch(branchManager.BranchId);
        //    ViewBag.BranchName = branchManager.Branch.Name;

        //    return View(leases);
        //}
    }
}