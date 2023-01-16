using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Services;
using System.Threading.Tasks;

namespace StefansSuperShop.Pages.Admin.Newsletters
{
    public class CreateModel : PageModel
    {
        private readonly INewsletterService _newsletterService;

        public CreateModel(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Newsletter Newsletter { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _newsletterService.CreateNewsletter(Newsletter.Title, Newsletter.Content);

            return RedirectToPage("./Index");
        }
    }
}
