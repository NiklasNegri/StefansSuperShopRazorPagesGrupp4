using Microsoft.AspNetCore.Identity;

namespace StefansSuperShop.Data
{
    public class AspNetUsers : IdentityUser
    {
        public bool NewslettersActive { get; set; }
    }
}
