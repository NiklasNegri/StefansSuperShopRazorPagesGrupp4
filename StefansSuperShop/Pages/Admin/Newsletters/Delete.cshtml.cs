﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Services;
using System.Threading.Tasks;

namespace StefansSuperShop.Pages.Admin.Newsletters
{
    public class DeleteModel : PageModel
    {
        private readonly INewsletterService _newsletterService;

        public DeleteModel(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        [BindProperty]
        public Newsletter Newsletter { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await _newsletterService.GetById(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _newsletterService.DeleteNewsletter(id);

            return RedirectToPage("./Index");
        }
    }
}
