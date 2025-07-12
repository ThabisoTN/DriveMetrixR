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
            try
            {
                
                var vehiclesBySupplier = await _dbContext.Vehicles
                    .Include(v => v.Supplier)
                    .GroupBy(v => v.Supplier.Name)
                    .Select(g => new
                    {
                        Supplier = g.Key,
                        VehicleCount = g.Count(),
                        PopularMake = g.GroupBy(v => v.Make)
                            .OrderByDescending(mg => mg.Count())
                            .Select(mg => mg.Key)
                            .FirstOrDefault(),
                        TotalValue = g.Sum(v => v.LeasingPrice)
                    })
                    .ToListAsync();

                
                var vehiclesByBranch = await _dbContext.Vehicles
                    .Include(v => v.Branch)
                    .GroupBy(v => v.Branch.Name)
                    .Select(g => new
                    {
                        Branch = g.Key,
                        TotalVehicles = g.Count(),
                        Available = g.Count(v => v.IsAvailable),
                        Leased = g.Count(v => !v.IsAvailable)
                    })
                    .ToListAsync();

                
                var leasesByClient = await _dbContext.Leases
                    .Include(l => l.Client)
                    .Include(l => l.Vehicle)
                    .Where(l => l.IsActive)
                    .GroupBy(l => l.Client.CompanyName)
                    .Select(g => new
                    {
                        Client = g.Key,
                        ActiveLeases = g.Count(),
                        Vehicles = string.Join(", ", g.Select(l => $"{l.Vehicle.Make} {l.Vehicle.Model}")),
                        MonthlyTotal = g.Sum(l => l.MonthlyRate)
                    })
                    .ToListAsync();

             
                var recentLeases = await _dbContext.Leases
                    .Include(l => l.Client)
                    .Include(l => l.Vehicle)
                    .OrderByDescending(l => l.StartDate)
                    .Take(5)
                    .Select(l => new
                    {
                        Vehicle = $"{l.Vehicle.Make} {l.Vehicle.Model}",
                        Client = l.Client.CompanyName,
                        StartDate = l.StartDate,
                        Status = l.LeaseStatus
                    })
                    .ToListAsync();

                
                var vehicleDistribution = await _dbContext.Vehicles
                    .GroupBy(v => v.Make)
                    .Select(g => new
                    {
                        Make = g.Key,
                        Count = g.Count()
                    })
                    .ToListAsync();

                
                ViewData["VehiclesBySupplier"] = vehiclesBySupplier;
                ViewData["VehiclesByBranch"] = vehiclesByBranch;
                ViewData["LeasesByClient"] = leasesByClient;
                ViewData["RecentLeases"] = recentLeases;
                ViewData["VehicleDistribution"] = vehicleDistribution;
                ViewData["TotalVehicles"] = await _dbContext.Vehicles.CountAsync();
                ViewData["TotalSuppliers"] = await _dbContext.Suppliers.CountAsync();
                ViewData["TotalBranches"] = await _dbContext.Branches.CountAsync();
                ViewData["ActiveClients"] = await _dbContext.Leases
                    .Where(l => l.IsActive)
                    .Select(l => l.ClientId)
                    .Distinct()
                    .CountAsync();

                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine( "Error loading dashboard data");
                return View("Error");
            }
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