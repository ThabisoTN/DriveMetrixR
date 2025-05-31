using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ProductAuthenticatorApp.Data
{
    public class DbInitializer
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            try
            {
                var roles = new[] { "Admin" };

                foreach (var roleName in roles)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        var role = new IdentityRole(roleName);
                        var result = await roleManager.CreateAsync(role);
                        if (!result.Succeeded)
                        {
                            Console.WriteLine($"Error creating role {roleName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        }
                            Console.WriteLine("Roles Seeded Successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured When Trying To Seed Roles : {ex.Message}");
            }
        }



        //Seed Type Of user
        public async static Task SeedUserType(ApplicationDbContext dbContext)
        {
            try
            {
                if (!dbContext.UserTypes.Any())
                {
                    var userTypes = new List<UserType>
                    {
                        new UserType { UserTypeName = "Individual" },
                        new UserType { UserTypeName = "Organization" }
                    };
                    await dbContext.UserTypes.AddRangeAsync(userTypes);
                    await dbContext.SaveChangesAsync();

                    Console.WriteLine("UserType Saved To database");
                }
                
                    Console.WriteLine("UserTypes Already Exist In the Database");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured When trying To Seed UserTypes : {ex.Message}");   
            }
        }


        //seed Admin Data 
        public async static Task SeedAdmin(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            try
            {
                var adminData = new ApplicationUser()
                {
                    Firstname = "Admin",
                    Lastname = "Admin",
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    UserTypeId = 1
                };
                var result = await userManager.CreateAsync(adminData, "Admin@1234");

                if (result.Succeeded)
                {

                    await userManager.AddToRoleAsync(adminData, "Admin");
                    Console.WriteLine("Admin user seeded successfully");
                }
                else
                {
                    Console.WriteLine($"Error creating admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured When Trying To Seed Admin data : {ex.Message}" );
            }
        }

        //Seed Product Category
        public async static Task SeedProductcategory( ApplicationDbContext dbContext)
        {
            try
            {
                if (!dbContext.ProductCategories.Any())
                {
                    var productCategoryData = new List<ProductCategory>()
                    {
                        new ProductCategory{CategoryName="Electronic"},
                        new ProductCategory {CategoryName="Other"}
                    };

                    await dbContext.ProductCategories.AddRangeAsync(productCategoryData);
                    await dbContext.SaveChangesAsync();

                    Console.WriteLine($"Product category Saved To database {productCategoryData.Count}");
                }
                Console.WriteLine("Product Categories Already Exist!!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occurred When Trying To Seed The Product category : {ex.Message}");
            }
        }
    }
}
