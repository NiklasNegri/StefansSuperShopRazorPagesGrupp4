using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.DTOs;
using StefansSuperShop.Services;

namespace StefansSuperShop.Pages.BackendTests.ApplicationUsers
{
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;

        public EditModel(IUserService userService)
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
            if (Model.NewEmail != null || Model.NewEmail != null)
            {
                await _userService.UpdateUser(Model);
            }
            else if (user.NewsletterActive != Model.NewsletterActive)
            {
                await _userService.UpdateNewsletterActive(Model);
            }

            return RedirectToPage("./Index");
        }
    }
}
