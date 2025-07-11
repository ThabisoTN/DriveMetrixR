using ProductAuthenticatorApp.Data;

namespace ProductAuthenticatorApp.Services
{
    public interface ILeaseService
    {
        
        Task<Lease> CreateLeaseRequest(
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
            string terms);
    }
}
