using Microsoft.AspNetCore.Identity;

namespace ProductAuthenticatorApp.Data
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { set;get; }
        public string LastName { set;get; }
        public bool IsBusinessClient { set;get; }
    }
}
