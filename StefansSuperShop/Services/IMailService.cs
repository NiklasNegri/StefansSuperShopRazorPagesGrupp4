using System.Threading;
using System.Threading.Tasks;
using StefansSuperShop.Model;

namespace StefansSuperShop.Services;

public interface IMailService
{
    public Task SendAsync(MailData mailData, CancellationToken ct);
}