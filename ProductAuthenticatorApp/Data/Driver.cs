using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;

namespace ProductAuthenticatorApp.Data
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
