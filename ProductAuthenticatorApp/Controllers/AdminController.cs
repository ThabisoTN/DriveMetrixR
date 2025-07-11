using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Service;
using ProductAuthenticatorApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ProductAuthenticatorApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductService _productService;
        private readonly IVehicleService vehicleService;
        private readonly IBranchService branchService;

        public AdminController(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            IProductService productService, IVehicleService vehicleService, IBranchService branchService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _productService = productService;
            this.vehicleService = vehicleService;
            this.branchService = branchService;
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


        //View Users
        public async Task<IActionResult> ViewApplicationUsers()
        {
            try
            {
                var getUsers = await _productService.GetApplicationUsers();
                return View(getUsers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting application users: {ex.Message}");
                return View(new List<ApplicationUser>());
            }
        }

    } }