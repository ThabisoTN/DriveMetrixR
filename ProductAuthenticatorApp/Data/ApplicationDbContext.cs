using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductAuthenticatorApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> applicationUsers { set; get; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
       public DbSet<Client> Clients { get; set; }
        public DbSet<BranchManager> BranchManagers { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Lease> Leases { get; set; }



    }
}

