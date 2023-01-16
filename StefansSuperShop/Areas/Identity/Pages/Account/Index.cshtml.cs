using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.DTOs;
using StefansSuperShop.Services;
using System.Threading.Tasks;

namespace StefansSuperShop.Areas.Identity.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public ApplicationUserDTO Model { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await _userService.GetById(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var user = await _userService.GetById(id);
            Model.Id = id;
            if (Model.Email != null || Model.Email != null)
            {
                await _userService.UpdateUser(Model);
            }
            else if (user.NewsletterIsActive != Model.NewsletterIsActive)
            {
                await _userService.UpdateNewsletterActive(Model);
            }

            return Page();
        }
    }
}
