using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAuthenticatorApp.Data
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(50)]
        public string TaxNumber { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }  // This must match the IdentityUser Id type

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
