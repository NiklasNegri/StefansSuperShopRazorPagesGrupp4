using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Services;
using System.Threading.Tasks;

namespace StefansSuperShop.Pages.Admin.Newsletters
{
    public class EditModel : PageModel
    {
        private readonly INewsletterService _newsletterService;

        public EditModel(INewsletterService newsletterService)
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
            await _newsletterService.EditNewsletter(id, Newsletter.Title, Newsletter.Content);

            return RedirectToPage("./Index");
        }
    }
}
