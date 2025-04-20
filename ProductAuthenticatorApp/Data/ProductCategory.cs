using System.ComponentModel.DataAnnotations;

namespace ProductAuthenticatorApp.Data
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; } 
        public string CategoryName { set; get; }
    }
}
