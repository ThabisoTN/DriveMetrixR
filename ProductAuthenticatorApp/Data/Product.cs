using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ProductAuthenticatorApp.Data
{
    public class Product
    {
        //Primary Key
        [Key]
        public int Id { set; get; }

        //Table Properties
        public string ProductName { set;get; }
        public string ProductDescription { set;get; }
        public int SerialNumber { set;get; }
        public string MACAddress { get; set; } 
        public string ModelNumber { get; set; }
        public string Manufacturer { get; set; }
        public byte[] ProductImage { get; set; }
        public string Color { get; set; }
        public string StorageCapacity { get; set; }    
        public string RAM { get; set; }                
        public string ProcessorModel { get; set; }
        public string OperatingSystem { get; set; }


        //Foreign Keys
        public string UserId { set; get; }
        public ApplicationUser ApplicationUser { get; set; }
        public string CategoryId {  set; get; }
        public ProductCategory ProductCategory { get; set; }
   
    }
}
