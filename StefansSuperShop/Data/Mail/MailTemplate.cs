namespace StefansSuperShop.Data.Mail;

using Microsoft.Extensions.Options;
using MimeKit;
using StefansSuperShop.Configuration;
using StefansSuperShop.Data.Model;
using System.IO;

public abstract class MailTemplate
{
    public string GetMailTemplateBody(string myBody)
    {
        string filePath = "C:\\source\\TH\\StefansSuperShopRazorPagesGrupp4\\StefansSuperShop\\Data\\Mail\\Templates\\StefansShopTemplate\\index.html";
        StreamReader str = new StreamReader(filePath);
        string MailText = str.ReadToEnd();
        str.Close();

        var email = new MimeMessage();

        var builder = new BodyBuilder();

        MailText = MailText.Replace("[body0]", "Upper Body").Replace("[body1]", myBody);
        //builder.HtmlBody = MailText;
        //email.Body = builder.ToMessageBody();


        return MailText;
    }
}
