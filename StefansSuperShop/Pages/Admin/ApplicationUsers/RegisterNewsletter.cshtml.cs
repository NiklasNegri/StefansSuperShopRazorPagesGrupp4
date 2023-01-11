using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.DTOs;
using StefansSuperShop.Services;
using System.Threading.Tasks;

namespace StefansSuperShop.Pages.Admin.ApplicationUsers
{
    public class RegisterNewsletterModel : PageModel
    {
        private readonly IUserService _userService;

        public RegisterNewsletterModel(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ApplicationUserDTO Model { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Model.UserName = Model.Email;
            await _userService.RegisterUser(Model);

            return RedirectToPage("./Index");
        }
    }
}
