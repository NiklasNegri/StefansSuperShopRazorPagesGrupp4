using Microsoft.Extensions.Options;
using MimeKit;
using StefansSuperShop.Configuration;
using StefansSuperShop.Data.Model;
using System.IO;

namespace StefansSuperShop.Data.Mail
{
    public class NewsletterTemplate : MailTemplate
    {
        public string GetNewsletterBody(string body)
        {
            string bodyTemplate = GetMailTemplateBody(body);
            return bodyTemplate;
        }
    }


}

