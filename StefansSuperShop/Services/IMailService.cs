using System.Threading;
using System.Threading.Tasks;
using StefansSuperShop.Model;

namespace StefansSuperShop.Services;

public interface IMailService
{
    Task<bool> SendAsync(MailData mailData, CancellationToken ct);
}