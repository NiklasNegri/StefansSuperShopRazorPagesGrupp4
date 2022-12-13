using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace StefansSuperShop.Mailtrap;

public class Mailtrapper
{
    public string UserName { get; set; }
    private readonly Uri _baseAddress;
    private const string _gmailServer = "smtp.gmail.com";
    private const string _outlookServer = "smtp-mail.outlook.com";
    public Mailtrapper()
    {
        _baseAddress = new Uri("https://private-anon-6c570fc9c4-mailtrap.apiary-proxy.com/");
    }
    
    public void FLCreateMessage(string server)
    {
        MailMessage message = new MailMessage(
            "stefans.supershop@gmail.com",
            "fredrik.lam@learnet.se",
            "Hello",
            "Din snygge jävel");
        SmtpClient client = new SmtpClient(server);
        client.Port = 587;

        client.Credentials = new NetworkCredential("9568381dda15d0", "9e0cc104e5ae08") as ICredentialsByHost;
        client.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };

        try
        {
            client.Send(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception caught in {nameof(CreateMessage)} {ex.ToString()}");
        }
    }

    public async Task<string> GetAllAccounts()
    {
        using (var httpClient = new HttpClient { BaseAddress = _baseAddress })
        {
            using (var response = await httpClient.GetAsync("api/accounts"))
            {
                return await response.Content.ReadAsStringAsync();
            }
        }
    }

    public async Task GetUserAsync()
    {
        var authToken = "cf48b4eb864a6b00c441d35ee6cce73a";
        using (var httpClient = new HttpClient { BaseAddress = _baseAddress })
        {
            //var request = new RestRequest(_baseAddress);
            //         request.AddHeader("Content-Type", "application/json");
            //         request.AddHeader("Authorization", authToken);


            using (var response = await httpClient.GetAsync("api/v1/user"))
            {
                string responseData = await response.Content.ReadAsStringAsync();
                UserName = responseData;
            }
        }
    }

    public async Task SendMail()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://sandbox.api.mailtrap.io/api/send/inbox_id"),
            Headers =
    {
        { "Api-Token", "cf48b4eb864a6b00c441d35ee6cce73a" },
    },
            Content = new StringContent("{\n  \"to\": [\n    {\n      \"email\": \"stefan\",\n      \"name\": \"John Doe\"\n    }\n  ],\n  \"from\": {\n    \"email\": \"stefans.supershop@gmail.com\",\n    \"name\": \"Example Sales Team\"\n  },\n  \"attachments\": [\n    {\n      \"content\": \"PCFET0NUWVBFIGh0bWw+CjxodG1sIGxhbmc9ImVuIj4KCiAgICA8aGVhZD4KICAgICAgICA8bWV0YSBjaGFyc2V0PSJVVEYtOCI+CiAgICAgICAgPG1ldGEgaHR0cC1lcXVpdj0iWC1VQS1Db21wYXRpYmxlIiBjb250ZW50PSJJRT1lZGdlIj4KICAgICAgICA8bWV0YSBuYW1lPSJ2aWV3cG9ydCIgY29udGVudD0id2lkdGg9ZGV2aWNlLXdpZHRoLCBpbml0aWFsLXNjYWxlPTEuMCI+CiAgICAgICAgPHRpdGxlPkRvY3VtZW50PC90aXRsZT4KICAgIDwvaGVhZD4KCiAgICA8Ym9keT4KCiAgICA8L2JvZHk+Cgo8L2h0bWw+Cg==\",\n      \"filename\": \"index.html\",\n      \"type\": \"text/html\",\n      \"disposition\": \"attachment\"\n    }\n  ],\n  \"custom_variables\": {\n    \"user_id\": \"47\",\n    \"batch_id\": \"PSJ-12\"\n  },\n  \"headers\": {\n    \"X-Message-Source\": \"dev.mydomain.com\"\n  },\n  \"subject\": \"Your Example Order Confirmation\",\n  \"text\": \"Congratulations on your order no. 1234\",\n  \"category\": \"API Test\"\n}")
            {
                Headers =
        {
            ContentType = new MediaTypeHeaderValue("application/json")
        }
            }
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);
        }
    }


    
    public void MailtrapTesterOnMailtrap() //Denna funkar till My Inbox Sandbox
    {
        var client = new SmtpClient("smtp.mailtrap.io", 2525)
        {
            Credentials = new NetworkCredential("9568381dda15d0", "9e0cc104e5ae08"),
            EnableSsl = true
        };
        client.Send("stefans.supershop@gmail.com", "fredrik.lam@learnet.se", "Snyggaste subject", "Din snyggaste fantablous mailtrap test");
        Console.WriteLine("Sent");
        Console.ReadLine();
    }

    public void MailtrapTesterOnOutlook()
    {
        var client = new SmtpClient("smtp.outlook.como", 587)
        {
            Credentials = new NetworkCredential("9568381dda15d0", "9e0cc104e5ae08"),
            EnableSsl = true
        };
        client.Send("stefans.supershop@outlook.com", "fredrik.lam@learnet.se", "Snyggaste subject", "Din snyggaste fantabilous mailtrap test på outlook");
        Console.WriteLine("Sent");
        Console.ReadLine();
    }

    public void TestClient() //Denna funkar till My Inbox Sandbox
    {
        var sender = new MailAddress("stefans.supershop@gmail.com");
        var recipient = new MailAddress("fredrik.lam@learnet.se");

        var email = new MailMessage(sender, recipient);
        email.Subject = "Testing out email sending";
        email.Body = "Mailtrap test";

        var smtp = new SmtpClient();
        smtp.Host = _outlookServer;
        smtp.Port = 587;
        smtp.Credentials = new NetworkCredential("9568381dda15d0", "9e0cc104e5ae08");
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.EnableSsl = true;

        try
        {
            /* Send method called below is what will send off our email 
             * unless an exception is thrown.
             */
            smtp.Send(email);
        }
        catch (SmtpException ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    public void CreateMessage()
    {
        const string from = "stefans.supershop@gmail.com";
        var message = new MailMessage(
            from,
            "niklas.o.lindblad+test@gmail.com",
            "Hello",
            "Din snygge jävel");
        var client = new SmtpClient(_gmailServer, 587);
        
        //const string accountPassword = "iV@309cbLShT";
		const string appPassword = "ytbqdfffihnowvvl";

		client.Credentials = new NetworkCredential(from, appPassword);
        client.UseDefaultCredentials = false;
        client.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            (s, certificate, chain, sslPolicyErrors) => true;

        try
        {
            client.Send(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception caught in {nameof(CreateMessage)} {ex}");
        }
    }
}