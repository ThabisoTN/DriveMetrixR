using System.ComponentModel.DataAnnotations;

namespace ProductAuthenticatorApp.Data
{
    public class Branch
    {
      
            [Key]
            public int BranchId { get; set; }

            [Required]
            [StringLength(100)]
            public string Name { get; set; }

            [StringLength(200)]
            public string Location { get; set; }

            [StringLength(100)]
            public string Manager { get; set; }

            [StringLength(20)]
            public string Phone { get; set; }

        
        
    }
}
