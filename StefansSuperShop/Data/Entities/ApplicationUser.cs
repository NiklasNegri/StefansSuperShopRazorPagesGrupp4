using Microsoft.AspNetCore.Identity;

namespace StefansSuperShop.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool NewsletterIsActive { get; set; }
    }
}
