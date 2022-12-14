using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.DTOs;
using StefansSuperShop.Services;
using System.Threading.Tasks;

namespace StefansSuperShop.Pages.BackendTests.ApplicationUsers
{
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;

        public CreateModel(IUserService userService)
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

            Model.UserName = Model.NewEmail;
            Model.Role = Request.Form["role"];
            await _userService.RegisterUser(Model);

            return RedirectToPage("./Index");
        }
    }
}
