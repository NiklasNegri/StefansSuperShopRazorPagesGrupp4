using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using StefansSuperShop.Configuration;
using StefansSuperShop.Model;

namespace StefansSuperShop.Services;

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
        try
        {
            var mail = new MimeMessage();

            #region Sender / Receiver
            // Sender
            mail.From.Add(new MailboxAddress(_settings.DisplayName, mailData.From ?? _settings.From));
            mail.Sender = new MailboxAddress(mailData.DisplayName ?? _settings.DisplayName, mailData.From ?? _settings.From);

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
            mail.Subject = mailData.Subject;
            body.HtmlBody = mailData.Body;
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
}