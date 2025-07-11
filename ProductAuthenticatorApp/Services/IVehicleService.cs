using ProductAuthenticatorApp.Data;

namespace ProductAuthenticatorApp.Services
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> GetAllVehicles();
        Task<Vehicle> GetVehicleById(int vehicleId);

    }
}
