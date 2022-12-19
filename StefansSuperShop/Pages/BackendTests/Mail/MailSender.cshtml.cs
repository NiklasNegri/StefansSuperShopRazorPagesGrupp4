using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StefansSuperShop.Model;
using StefansSuperShop.Services;
using System.Threading;
using System.Threading.Tasks;

namespace StefansSuperShop.Pages.BackendTests.Mail;

// https://localhost:44338/BackendTests/Mail/MailSender
public class MailSenderModel : PageModel
{
    private readonly IMailService _mailService;

    public MailSenderModel(IMailService mailService)
    {
        _mailService = mailService;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public MailData Model { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if(!ModelState.IsValid) 
            return Page();

        await _mailService.SendAsync(Model, new CancellationToken());

        return Page();
    }
}
