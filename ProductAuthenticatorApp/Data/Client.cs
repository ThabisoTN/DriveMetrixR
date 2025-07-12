using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAuthenticatorApp.Data
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string TaxNumber { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
