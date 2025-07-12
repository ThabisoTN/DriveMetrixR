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
    public class AdminService : IProductService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminService(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.roleManager = roleManager;
        }


        //Get All Suppliers 
        public async Task<List<Supplier>> GetSuppliers()
        {
            try
            {
                var suppliers = await dbContext.Suppliers.ToListAsync();

                if (suppliers != null)
                {
                    Console.WriteLine("Suppliers Retrieved Successfully");
                    return suppliers;
                }

                return new List<Supplier>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured When Trying To Get Suppliers : {ex.Message}");
                throw;
            }
        }




        //Get All Branches
        public async Task<List<Branch>> GetBranches()
        {
            try
            {
                var branches = await dbContext.Branches.ToListAsync();

                if (branches != null)
                {
                    Console.WriteLine("branches Retrieved Successfully");
                    return branches;
                }

                return new List<Branch>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured When Trying To Get Suppliers : {ex.Message}");
                throw;
            }
        }

        //get all users
        public async Task<List<ApplicationUser>> GetApplicationUsers()
        {
            try
            {
                var users = await dbContext.Users.ToListAsync();
                if (users != null)
                {
                    Console.WriteLine("Users Retrieved Successfully");
                    return users;
                }
                return new List<ApplicationUser>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured When Trying To Get Users : {ex.Message}");
                throw;
            }
        }





        public async Task<ApplicationUser> AddBranchManager( string email,string password,string firstName, string lastName, int branchId)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                
                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentException("Email is required");

                if (string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("Password is required");

          
                var branch = await dbContext.Branches.FindAsync(branchId);
                if (branch == null)
                {
                    var availableBranches = await dbContext.Branches
                        .Select(b => new { b.BranchId, b.Name })
                        .ToListAsync();

                    throw new ArgumentException(
                        $"Invalid branch specified. Available branches: {string.Join(", ", availableBranches.Select(b => $"{b.Name} (ID: {b.BranchId})"))}");
                }


                if (await userManager.FindByEmailAsync(email) != null)
                {
                    throw new InvalidOperationException("Email already registered");
                }

                
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    EmailConfirmed = true
                };

                
                var result = await userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    throw new Exception($"User creation failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
                
                const string roleName = "BranchManager";
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }

                
                var roleResult = await userManager.AddToRoleAsync(user, roleName);
                if (!roleResult.Succeeded)
                {
                    await userManager.DeleteAsync(user);
                    throw new Exception("Failed to assign role");
                }

                
                dbContext.BranchManagers.Add(new BranchManager
                {
                    BranchId = branchId,
                    ApplicationuserId = user.Id
                });

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return user;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}

