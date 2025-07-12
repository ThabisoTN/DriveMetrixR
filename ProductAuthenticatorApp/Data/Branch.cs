using System.ComponentModel.DataAnnotations;

namespace ProductAuthenticatorApp.Data
{
    public class Branch
    {
      
            [Key]
            public int BranchId { get; set; }
            public string Name { get; set; }
            public string Location { get; set; }
            public string Manager { get; set; }
            public string Phone { get; set; }

        
        
    }
}
