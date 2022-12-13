using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Services;

namespace StefansSuperShop.Pages.BackendTests.ApplicationUsers
{
    public class DeleteModel : PageModel
    {
        private readonly IUserService _userService;

        public DeleteModel(IUserService userService)
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
            _userService.DeleteUser(id);

            return RedirectToPage("./Index");
        }
    }
}
