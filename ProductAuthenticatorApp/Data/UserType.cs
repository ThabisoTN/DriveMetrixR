using System.ComponentModel.DataAnnotations;

namespace ProductAuthenticatorApp.Data
{
    public class UserType
    {
        [Key]
        public int Id { set; get; }
        public string UserTypeName { set; get; }
    }
}
