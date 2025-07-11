using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAuthenticatorApp.Data
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string VIN { get; set; }
        public string Color { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal LeasingPrice { get; set; }
        public string ImagePath { get; set; }
        public bool IsAvailable { get; set; } = true;   

        // Foreign keys
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }

        // Navigation properties
        public virtual Supplier Supplier { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
