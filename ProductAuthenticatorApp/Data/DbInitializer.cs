using Microsoft.AspNetCore.Identity;

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
                        else
                        {
                            Console.WriteLine("Roles Seeded Successfully");
                        }
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
                else
                {
                    Console.WriteLine("UserTypes Already Exist In the Database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured When trying To Seed UserTypes : {ex.Message}");   
            }
        }
    }
}
