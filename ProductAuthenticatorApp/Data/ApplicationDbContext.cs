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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This is crucial for Identity

            // Fix for IdentityUserLogin issue
            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            });

            // Configure Lease entity relationships
            modelBuilder.Entity<Lease>(entity =>
            {
                entity.HasOne(l => l.Branch)
                      .WithMany()
                      .HasForeignKey(l => l.BranchId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(l => l.Vehicle)
                      .WithMany()
                      .HasForeignKey(l => l.VehicleId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(l => l.Client)
                      .WithMany()
                      .HasForeignKey(l => l.ClientId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(l => l.Driver)
                      .WithMany()
                      .HasForeignKey(l => l.DriverId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure decimal precision for Vehicle
            modelBuilder.Entity<Vehicle>()
                .Property(v => v.LeasingPrice)
                .HasPrecision(18, 2);
        }
    }
    }

