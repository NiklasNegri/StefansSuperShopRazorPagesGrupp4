using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.DTOs;
using StefansSuperShop.Services;
using System.Threading.Tasks;

namespace StefansSuperShop.Pages.Admin.ApplicationUsers
{
    public class DeleteModel : PageModel
    {
        private readonly IUserService _userService;

        public DeleteModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public ApplicationUserDTO Model { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            _userService.GetById(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            await _userService.DeleteUser(id);

            return RedirectToPage("./Index");
        }
    }
}
