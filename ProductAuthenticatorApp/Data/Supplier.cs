using System.ComponentModel.DataAnnotations;

namespace ProductAuthenticatorApp.Data
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}

