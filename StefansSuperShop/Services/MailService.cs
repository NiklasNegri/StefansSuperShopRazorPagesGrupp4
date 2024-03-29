using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using StefansSuperShop.Configuration;
using StefansSuperShop.Data.Model;

namespace StefansSuperShop.Services;

public interface IMailService
{
    public Task SendAsync(MailData mailData, CancellationToken ct);
}

// Based on this article https://blog.christian-schou.dk/send-emails-with-asp-net-core-with-mailkit/
// Testaccount creata an account at https://ethereal.email/create and use that address as the recipient
public class MailService : IMailService
{
    private MailSettings _settings;
    public MailService(IOptions<MailSettings> settings)
    {
        _settings = settings.Value;
    }
    
    public async Task SendAsync(MailData mailData, CancellationToken ct)
    {
        if (mailData.To == null)
        {
            throw new ArgumentNullException(nameof(mailData));
        }

        if (mailData.Subject == null)
        {
            throw new ArgumentNullException(nameof(mailData));
        }

        try
        {
            var mail = new MimeMessage();

            #region Sender / Receiver
            // Sender
            
            mail.From.Add(new MailboxAddress(mailData.DisplayName, mailData.From));

            // Set Reply to if specified in mail data
            if(!string.IsNullOrEmpty(mailData.ReplyTo))
                mail.ReplyTo.Add(new MailboxAddress(mailData.ReplyToName, mailData.ReplyTo));
            
            // Receiver
            AddRecipient(mail.To, mailData.To);

            // CC
            AddRecipient(mail.Cc, mailData.Cc);

            // BCC
            AddRecipient(mail.Bcc, mailData.Bcc);
            #endregion

            #region Content

            // Add Content to Mime Message
            var body = new BodyBuilder();
            mail.Subject = mailData.Subject.Trim().ToUpper();
            body.HtmlBody = GetMailTemplate(mailData.Body).Trim();
            mail.Body = body.ToMessageBody();

            #endregion

            #region Send Mail

            using var smtp = new SmtpClient();

            if (_settings.UseSSL)
            {
                await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.SslOnConnect, ct);
            }
            else if (_settings.UseStartTls)
            {
                await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, ct);
            }
            await smtp.AuthenticateAsync(_settings.UserName, _settings.Password, ct);
            await smtp.SendAsync(mail, ct);
            await smtp.DisconnectAsync(true, ct);
            
            #endregion

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    private static void AddRecipient(InternetAddressList mailList, IEnumerable<string> mailData)
    {
        foreach (var mailAddress in mailData.Where(x => !string.IsNullOrWhiteSpace(x)))
            mailList.Add(MailboxAddress.Parse(mailAddress.Trim()));
    }

    private static string GetMailTemplate(string body)
    {
        string filePath1 = "Data/Mail/Templates/index.html";
        StreamReader str = new StreamReader(filePath1);
        string mailTemplateText = str.ReadToEnd();
        str.Close();

        mailTemplateText = mailTemplateText.Replace("[This text till be replaced with input from user]", body);

        return mailTemplateText;
    }
}
