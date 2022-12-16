using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(StefansSuperShop.Areas.Identity.IdentityHostingStartup))]
namespace StefansSuperShop.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}