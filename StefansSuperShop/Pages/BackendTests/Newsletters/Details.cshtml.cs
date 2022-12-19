using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Services;
using System.Threading.Tasks;

namespace StefansSuperShop.Pages.BackendTests.Newsletters
{
    public class DetailsModel : PageModel
    {
        private readonly INewsletterService _newsletterService;

        public DetailsModel(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        public Newsletter Newsletter { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await _newsletterService.GetById(id);

            return Page();
        }
    }
}
