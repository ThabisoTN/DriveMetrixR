﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductAuthenticatorApp.Data;
using ProductAuthenticatorApp.Service;

namespace ProductAuthenticatorApp.Services
{
    public class VehicleService: IVehicleService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<AdminService> logger;

        public VehicleService(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, ILogger<AdminService> logger)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.logger = logger;
        }


        //Get All Vehicles
        public async Task<List<Vehicle>> GetAllVehicles()
        {
            try
            {
                return await dbContext.Vehicles
                    .Include(v => v.Branch)
                    .Include(v => v.Supplier)
                    .OrderBy(v => v.Make)
                    .ThenBy(v => v.Model)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error occurred while getting all vehicles");
                throw;
            }
        }


        //Find Vehicle by Id

        public async Task<Vehicle> GetVehicleById(int vehicleId)
        {
            try
            {
                var vehicle = await dbContext.Vehicles.FindAsync(vehicleId);
                if (vehicle == null)
                {
                    Console.WriteLine($"Vehicle with ID {vehicleId} not found.");
                    return null;
                }
                return vehicle;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, $"Error retrieving vehicle with ID {vehicleId}.");
                throw;
            }
        }

    }
}
