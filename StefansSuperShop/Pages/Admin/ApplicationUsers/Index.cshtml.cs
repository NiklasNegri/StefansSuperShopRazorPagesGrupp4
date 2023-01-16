using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.DTOs;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StefansSuperShop.Pages.Admin.ApplicationUsers
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public IList<ApplicationUserDTO> ApplicationUsers { get; set; }

        public async Task OnGetAsync()
        {
            var users = await _userService.GetAllUsersAndRoles();
            ApplicationUsers = users.ToList();
        }
    }
}
