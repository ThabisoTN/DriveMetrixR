using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAuthenticatorApp.Data
{
    public class Lease
    {
        [Key]
        public int LeaseId { get; set; }
        
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Terms { get; set; }
       
        public string LeaseStatus { get; set; } = "RequestSent";

        
        public DateTime RequestDate { get; set; } = DateTime.Now;

       
        public DateTime? ApprovalDate { get; set; }

       
        public bool IsActive { get; set; } = true;

        public decimal MonthlyRate { get; set; }

        public string ClientUserId { get; set; }

        public int BranchId { get; set; }
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

  
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        public int DriverId { get; set; }
        public virtual Driver Driver { get; set; }
    }
}
