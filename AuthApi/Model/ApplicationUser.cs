using Microsoft.AspNetCore.Identity;

namespace AuthApi.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
