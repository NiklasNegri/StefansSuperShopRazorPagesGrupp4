using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StefansSuperShop.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Active Newsletter")]
        public bool NewsletterIsActive { get; set; }
    }
}
