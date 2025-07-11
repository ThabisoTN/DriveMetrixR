using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Service;

namespace ProductAuthenticatorApp.Services
{
    public class LeaseService:ILeaseService
    {
            private readonly ApplicationDbContext _context;
            private readonly IVehicleService vehicleService;

            public LeaseService(ApplicationDbContext context, IVehicleService vehicleService)
            {
                _context = context;
                this.vehicleService =vehicleService;
            }

        public async Task<Lease> CreateLeaseRequest(
        int vehicleId,
        string applicationUserId,
        string driverName,
        string driverLicenseNumber,
        string driverEmail,
        string driverPhone,
        string driverAddress,
        DateTime? driverDateOfBirth,
        DateTime startDate,
        DateTime? endDate,
        string terms)
        {
            // Get client from application user
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.ApplicationUserId == applicationUserId);

            if (client == null)
            {
                throw new InvalidOperationException("Client record not found");
            }

            // Check vehicle availability
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);
            if (vehicle == null || !vehicle.IsAvailable)
            {
                throw new InvalidOperationException("Vehicle is not available for leasing");
            }

            // Create driver
            var driver = new Driver
            {
                Name = driverName,
                LicenseNumber = driverLicenseNumber,
                Email = driverEmail,
                Phone = driverPhone,
                Address = driverAddress,
                DateOfBirth = driverDateOfBirth
            };

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            // Create lease
            var lease = new Lease
            {
                VehicleId = vehicleId,
                BranchId = vehicle.BranchId,
                ClientId = client.ClientId,
                DriverId = driver.DriverId,
                StartDate = startDate,
                EndDate = endDate,
                MonthlyRate = vehicle.LeasingPrice,
                Terms = terms,
                LeaseStatus = "RequestSent",
                RequestDate = DateTime.Now,
                IsActive = true
            };

            _context.Leases.Add(lease);

            // Mark vehicle as unavailable
            vehicle.IsAvailable = false;
            _context.Vehicles.Update(vehicle);

            await _context.SaveChangesAsync();

            Console.WriteLine("Lease Has been submitted!!!");

            return lease;
        }



    }
}
