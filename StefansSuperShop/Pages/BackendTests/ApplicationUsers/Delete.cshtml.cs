﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.DTOs;
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
        public ApplicationUserDTO Model { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await _userService.GetById(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            await _userService.DeleteUser(id);

            return RedirectToPage("./Index");
        }
    }
}
