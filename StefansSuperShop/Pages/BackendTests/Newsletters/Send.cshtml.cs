using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Services;
using System.Threading.Tasks;

namespace StefansSuperShop.Pages.BackendTests.Newsletters
{
    public class SendModel : PageModel
    {
        private readonly INewsletterService _newsletterService;

        public SendModel(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        [BindProperty]
        public Newsletter Newsletter { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await _newsletterService.GetById(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _newsletterService.CreateSentNewsletter(id);

            return RedirectToPage("./Index");
        }
    }
}
