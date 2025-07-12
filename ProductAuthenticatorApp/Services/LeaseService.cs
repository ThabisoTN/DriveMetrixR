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

        



    }
}
