using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;

namespace ProductAuthenticatorApp.Data
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "License Number")]
        public string LicenseNumber { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        // Navigation property
        public virtual ICollection<Lease> Leases { get; set; }
    }
}
