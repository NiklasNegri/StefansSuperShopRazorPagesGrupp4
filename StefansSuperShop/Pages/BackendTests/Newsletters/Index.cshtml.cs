using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StefansSuperShop.Pages.BackendTests.Newsletters
{
    public class IndexModel : PageModel
    {
        private readonly INewsletterService _newsletterService;

        public IndexModel(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        public IList<Newsletter> Newsletters { get;set; }

        public async Task OnGetAsync()
        {
            var newsletters = await _newsletterService.GetAll();
            Newsletters = newsletters.ToList();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _newsletterService.CreateSentNewsletter(id);
            return Page();
        }
    }
}
