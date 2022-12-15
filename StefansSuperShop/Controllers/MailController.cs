using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StefansSuperShop.Model;
using StefansSuperShop.Services;

namespace StefansSuperShop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MailController : ControllerBase
{
    private readonly IMailService _mail;

    public MailController(IMailService mail)
    {
        _mail = mail;
    }

    [HttpPost("sendmail")]
	[AllowAnonymous]
    public async Task<IActionResult> SendMailAsync(MailData mailData)
    {
        var result = await _mail.SendAsync(mailData, new CancellationToken());

        if (result)
        {
            return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
        }

        return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
    }
}
