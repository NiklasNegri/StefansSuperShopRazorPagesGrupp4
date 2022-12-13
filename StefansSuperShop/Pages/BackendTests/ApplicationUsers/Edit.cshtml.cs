using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.Entities;
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
        public ApplicationUser ApplicationUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            _userService.GetByEmail(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            _userService.UpdateUser(id, ApplicationUser.Email, ApplicationUser.PasswordHash);

            return RedirectToPage("./Index");
        }
    }
}
