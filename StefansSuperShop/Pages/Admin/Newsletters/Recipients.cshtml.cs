using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StefansSuperShop.Pages.Admin.Newsletters
{
    public class RecipientsModel : PageModel
    {
        private readonly INewsletterService _newsletterService;
        
        public RecipientsModel(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }
        public NewsletterSent NewsletterSent { get; set; }
        public List<ApplicationUser> Recipients { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var newsletterSent = await _newsletterService.GetByIdSent(id);
            NewsletterSent = newsletterSent;

            var recipients = await _newsletterService.GetAllRecipients(id);
            Recipients = recipients.ToList();

            return Page();
        }
    }
}
