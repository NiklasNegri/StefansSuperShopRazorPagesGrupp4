using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace StefansSuperShop.Mailtrap;

public class Mailtrapper
{
    private const string _gmailServer = "smtp.gmail.com";
    
    public void CreateMessage(List<string> recipients, string subject, string body)
    {
        const string from = "stefans.supershop@gmail.com";

		var encoding = Encoding.Latin1;

		var contentType = new ContentType("text/html");
		contentType.CharSet = encoding.WebName;
		var altView = AlternateView.CreateAlternateViewFromString(body, contentType);

		var message = new MailMessage();
		message.From = new MailAddress(from);
		message.Subject = subject;
		message.SubjectEncoding = encoding;
		message.Body = "This message uses html encoding. Please enable to view content";
		message.AlternateViews.Add(altView);

		recipients.ForEach(recipient => message.Bcc.Add(recipient));

        var client = new SmtpClient(_gmailServer, 587);

		const string appPassword = "ytbqdfffihnowvvl";

		client.Credentials = new NetworkCredential(from, appPassword);
        client.UseDefaultCredentials = false;
        client.EnableSsl = true;

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