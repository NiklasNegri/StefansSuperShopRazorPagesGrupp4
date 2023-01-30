using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Data.Model;
using StefansSuperShop.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StefansSuperShop.Pages.Contact
{
    // https://localhost:44338/Contact/
    public class IndexModel : PageModel
    {
        private readonly IMailService _mailService;
        private readonly IConfiguration config;

        public IndexModel(IMailService mailService, IConfiguration config)
        {
            _mailService = mailService;
            this.config = config;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MailData Model { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var emailTo = this.config.GetValue<string>("MailSettings:To");
            Model.To = new List<string> { emailTo };

            await _mailService.SendAsync(Model, new CancellationToken());

            return RedirectToPage("/Contact/Success");
        }
    }
}
