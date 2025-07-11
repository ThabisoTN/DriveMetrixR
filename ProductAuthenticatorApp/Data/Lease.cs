using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAuthenticatorApp.Data
{
    public class Lease
    {
        [Key]
        public int LeaseId { get; set; }
        [Required]
        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        [Required]
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [ForeignKey("Driver")]
        public int? DriverId { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyRate { get; set; }

        [StringLength(500)]
        public string Terms { get; set; }
        [Display(Name = "Status")]
        public string LeaseStatus { get; set; } = "RequestSent";

        [Display(Name = "Request Date")]
        public DateTime RequestDate { get; set; } = DateTime.Now;

        [Display(Name = "Approval Date")]
        public DateTime? ApprovalDate { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        



        public virtual Branch Branch { get; set; }

        public virtual Vehicle Vehicle { get; set; }
        public virtual Client Client { get; set; }
        public virtual Driver Driver { get; set; }
    }
}
