using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Services;

namespace StefansSuperShop.Pages.BackendTests.Newsletters
{
    public class DetailsModel : PageModel
    {
        private readonly INewsletterService _newsletterService;

        public DetailsModel(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        public Newsletter Newsletter { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            _newsletterService.GetById(id);

            return Page();
        }
    }
}
