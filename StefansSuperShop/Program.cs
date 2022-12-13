using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StefansSuperShop.Data;
using StefansSuperShop.Mailtrap;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace StefansSuperShop;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using (var scope = host.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var dataInitializer = serviceProvider.GetRequiredService<DataInitializer>();
            dataInitializer.SeedData();
        }
        MailtrapTester();

        host.Run();

    }

    private static async Task MailtrapTester()
    {
        Mailtrapper mailtrap = new();
        //mailtrap.MailtrapTesterOnMailtrap();
        //mailtrap.MailtrapTesterOnOutlook();
        //mailtrap.CreateMessage("smtp.gmail.com");
        //mailtrap.FLCreateMessage("smtp.office365.com");
        mailtrap.CreateMessage();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}