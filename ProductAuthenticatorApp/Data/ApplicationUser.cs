using Microsoft.AspNetCore.Identity;

namespace ProductAuthenticatorApp.Data
{
    public class ApplicationUser: IdentityUser
    {
        public string Firstname { set;get; }
        public string Lastname { set;get; }

        public int UserTypeId { set;get; }

    }
}
