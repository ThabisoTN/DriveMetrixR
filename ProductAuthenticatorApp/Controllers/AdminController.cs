using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Service;
using ProductAuthenticatorApp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAuthenticatorApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductService _productService;
        private readonly IVehicleService vehicleService;

        public AdminController(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            IProductService productService, IVehicleService vehicleService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _productService = productService;
            this.vehicleService = vehicleService;
        }


        //Landing page for admin dashboard
        public async Task<IActionResult> Index()
        {
            var vehicles = await vehicleService.GetAllVehicles();
            return View(vehicles);
        }

        [HttpGet]
        public async Task<IActionResult> ViewSuppliers()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                {
                    Console.WriteLine("Error: User not authenticated");
                    throw new UnauthorizedAccessException("User not authenticated");
                }

                Console.WriteLine($"Getting suppliers");
                var suppliers = await _productService.GetSuppliers();
                Console.WriteLine($"Successfully retrieved suppliers");
                return View(suppliers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting suppliers: {ex.Message}");
                return View(new List<Supplier>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewBranches()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                {
                    Console.WriteLine("Error: User not authenticated");
                    throw new UnauthorizedAccessException("User not authenticated");
                }

                Console.WriteLine($"Getting branches for user {userId}");
                var branches = await _productService.GetBranches();
                Console.WriteLine($"Successfully retrieved {branches?.Count ?? 0} branches");
                return View(branches);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting branches: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return View(new List<Branch>());
            }
        }



        [HttpGet]
        public async Task<IActionResult> AddBranchManager()
        {
            try
            {
                var branches = await _productService.GetBranches();
                ViewBag.Branches = new SelectList(branches, "BranchId", "Name");
                return View();
            }
            catch (Exception ex)
            {
                // Log the error properly in production
                Console.WriteLine($"Error loading branches: {ex.Message}");

                // Provide empty list to prevent null reference
                ViewBag.Branches = new SelectList(new List<Branch>(), "BranchId", "Name");
                return View();
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddBranchManager(
                string email,
                string password,
                string confirmPassword,
                string firstName,
                string lastName,
                int branchId)
        {
            try
            {
                if (password != confirmPassword)
                    ModelState.AddModelError("", "Passwords do not match");

                if (!ModelState.IsValid)
                {
                    ViewBag.Branches = await _productService.GetBranches();
                    return View();
                }

                var newManager = await _productService.AddBranchManager(
                    email, password, firstName, lastName, branchId);

                TempData["SuccessMessage"] = $"Added new branch manager: {newManager.Email}";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Branches = await _productService.GetBranches();
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

    }
}