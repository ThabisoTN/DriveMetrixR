using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace ProductAuthenticatorApp.Data
{
    public class DbInitializer
    {
        //Seed Roles
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            try
            {
                var roles = new[] { "Admin","Client" };

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


        //seed Admin Data 
        public async static Task SeedAdmin(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            try
            {
                var adminData = new ApplicationUser()
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
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
                Console.WriteLine($"Error Occured When Trying To Seed Admin data : {ex.Message}");
            }
        }

        public static async Task SeedSuppliers(ApplicationDbContext dbcontext)
        {
            try
            {
                // First ensure database is created and migrated
                await dbcontext.Database.EnsureCreatedAsync();

                if (await dbcontext.Suppliers.AnyAsync())
                {
                    Console.WriteLine("Suppliers already exist in the database. Skipping seeding.");
                    return;
                }

                var suppliers = new List<Supplier>
                {
                    new Supplier
                    {
                        Name = "BMW Durban",
                        ContactPerson = "James Khumalo",
                        Email = "sales@bmwdurban.co.za",
                        Phone = "+27 31 123 4567",
                        Address = "123 Umhlanga Rocks Dr, Durban, 4001"
                    },
                    new Supplier
                    {
                        Name = "Toyota Johannesburg",
                        ContactPerson = "Sarah van der Merwe",
                        Email = "info@toyotajhb.co.za",
                        Phone = "+27 11 234 5678",
                        Address = "45 Main Rd, Johannesburg, 2000"
                    },
                    new Supplier
                    {
                        Name = "VW Cape Town",
                        ContactPerson = "Mohammed Patel",
                        Email = "leasing@vwct.co.za",
                        Phone = "+27 21 345 6789",
                        Address = "7 Harbour Ave, Cape Town, 8001"
                    },
                    new Supplier
                    {
                        Name = "Mercedes-Benz Pretoria",
                        ContactPerson = "Anna Botha",
                        Email = "fleet@mbpretoria.co.za",
                        Phone = "+27 12 456 7890",
                        Address = "33 Church St, Pretoria, 0002"
                    },
                    new Supplier
                    {
                        Name = "Nissan Port Elizabeth",
                        ContactPerson = "David Williams",
                        Email = "fleet@nissanpe.co.za",
                        Phone = "+27 41 567 8901",
                        Address = "18 Beach Rd, Port Elizabeth, 6001"
                    }
                };

                await dbcontext.Suppliers.AddRangeAsync(suppliers);
                await dbcontext.SaveChangesAsync();

                Console.WriteLine($"{suppliers.Count} suppliers seeded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding suppliers: {ex.Message}");
            }
        }



        //Seed Branches
        public static async Task SeedBranches(ApplicationDbContext context)
        {
            try
            {

                if (await context.Branches.AnyAsync())
                {
                    Console.WriteLine("Branches already exist in the database. Skipping seeding.");
                    return;
                }

                var branches = new List<Branch>
            {
                new Branch
                {
                    Name = "Johannesburg Main",
                    Location = "123 Business Park, Sandton, Johannesburg",
                    Manager = "Thabo Mbeki",
                    Phone = "+27 11 123 4567"
                },
                new Branch
                {
                    Name = "Cape Town Coastal",
                    Location = "45 Beach Road, Waterfront, Cape Town",
                    Manager = "Nomsa Khumalo",
                    Phone = "+27 21 234 5678"
                },
                new Branch
                {
                    Name = "Durban Harbour",
                    Location = "7 Harbour View, Umhlanga, Durban",
                    Manager = "Raj Patel",
                    Phone = "+27 31 345 6789"
                },
                new Branch
                {
                    Name = "Pretoria Central",
                    Location = "33 Government Avenue, Pretoria",
                    Manager = "Anna van der Merwe",
                    Phone = "+27 12 456 7890"
                },
                new Branch
                {
                    Name = "Port Elizabeth East",
                    Location = "18 Marine Drive, Summerstrand, Port Elizabeth",
                    Manager = "David Williams",
                    Phone = "+27 41 567 8901"
                }
            };

                await context.Branches.AddRangeAsync(branches);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding branches: {ex.Message}");
            }
        }




        //Seed Vehicles
        public static async Task SeedVehicles(ApplicationDbContext context)
        {
            try
            {
                // First ensure database is created and migrated
                await context.Database.EnsureCreatedAsync();

                if (await context.Vehicles.AnyAsync())
                {
                    Console.WriteLine("Vehicles already exist in the database. Skipping seeding.");
                    return;
                }

                // Get existing branches and suppliers to assign to vehicles
                var branches = await context.Branches.ToListAsync();
                var suppliers = await context.Suppliers.ToListAsync();

                if (!branches.Any() || !suppliers.Any())
                {
                    Console.WriteLine("Cannot seed vehicles - branches or suppliers not found. Seed them first.");
                    return;
                }

                var vehicles = new List<Vehicle>
                {
                    new Vehicle
                    {
                Make = "BMW",
                Model = "320i",
                Year = 2022,
                VIN = "WBA8E9C07JFV12345",
                Color = "Alpine White",
                PurchaseDate = DateTime.Parse("2022-03-15"),
                LeasingPrice = 8500.00m,
                ImagePath = "/Images/BMW 320I.jpg",
                SupplierId = suppliers[0].SupplierId, // BMW Durban
                BranchId = branches[2].BranchId // Durban Harbour
            },
            new Vehicle
            {
                Make = "Toyota",
                Model = "Corolla",
                Year = 2023,
                VIN = "JTNKARJE8P3123456",
                Color = "Silver Metallic",
                PurchaseDate = DateTime.Parse("2023-01-10"),
                LeasingPrice = 6500.00m,
                ImagePath = "/Images/Toyota Corola.jpg",
                SupplierId = suppliers[1].SupplierId, // Toyota Johannesburg
                BranchId = branches[0].BranchId // Johannesburg Main
            },
            new Vehicle
            {
                Make = "Volkswagen",
                Model = "Polo",
                Year = 2022,
                VIN = "WVWZZZ6RZGY123456",
                Color = "Deep Black",
                PurchaseDate = DateTime.Parse("2022-07-22"),
                LeasingPrice = 5500.00m,
                ImagePath = "/Images/VW Polo.jpg",
                SupplierId = suppliers[2].SupplierId, // VW Cape Town
                BranchId = branches[1].BranchId // Cape Town Coastal
            },
            new Vehicle
            {
                Make = "Mercedes-Benz",
                Model = "C200",
                Year = 2023,
                VIN = "WDDWF8CB7PF123456",
                Color = "Selenite Grey",
                PurchaseDate = DateTime.Parse("2023-02-18"),
                LeasingPrice = 9500.00m,
                ImagePath = "/Images/Mercedes CL200.jpg",
                SupplierId = suppliers[3].SupplierId, // Mercedes-Benz Pretoria
                BranchId = branches[3].BranchId // Pretoria Central
            },
            new Vehicle
            {
                Make = "Nissan",
                Model = "Qashqai",
                Year = 2022,
                VIN = "SJNFAAJ12U7123456",
                Color = "Magnetic Blue",
                PurchaseDate = DateTime.Parse("2022-11-05"),
                LeasingPrice = 6000.00m,
                ImagePath = "/Images/Nissani QashQai.jpg",
                SupplierId = suppliers[4].SupplierId, // Nissan Port Elizabeth
                BranchId = branches[4].BranchId // Port Elizabeth East
            },
            new Vehicle
            {
                Make = "BMW",
                Model = "X3",
                Year = 2023,
                VIN = "5UXTR9C07P9A12345",
                Color = "Phytonic Blue",
                PurchaseDate = DateTime.Parse("2023-04-12"),
                LeasingPrice = 10500.00m,
                ImagePath = "/Images/BMW X3.jpg",
                SupplierId = suppliers[0].SupplierId, // BMW Durban
                BranchId = branches[0].BranchId // Johannesburg Main
            },
            new Vehicle
            {
                Make = "Toyota",
                Model = "Hilux",
                Year = 2022,
                VIN = "AHTFX58G4EL123456",
                Color = "Graphite Grey",
                PurchaseDate = DateTime.Parse("2022-09-30"),
                LeasingPrice = 7500.00m,
                ImagePath = "/Images/Toyota Hilux.jpg",
                SupplierId = suppliers[1].SupplierId, // Toyota Johannesburg
                BranchId = branches[3].BranchId // Pretoria Central
            }
        };

                await context.Vehicles.AddRangeAsync(vehicles);
                await context.SaveChangesAsync();

                Console.WriteLine($"{vehicles.Count} vehicles seeded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding vehicles: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
        }
    }
}

