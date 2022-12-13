using Microsoft.AspNetCore.Identity;

namespace StefansSuperShop.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool NewsletterActive { get; set; }
    }
}
