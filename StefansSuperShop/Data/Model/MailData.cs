using System.Collections.Generic;

namespace StefansSuperShop.Data.Model;

public class MailData
{
    // Receiver
    public List<string> To { get; set; } = new List<string>();
    public List<string> Cc { get; set; } = new List<string>();
    public List<string> Bcc { get; set; } = new List<string>();

    // Sender
    public string From { get; set; }
    public string DisplayName { get; set; }
    public string ReplyTo { get; set; }
    public string ReplyToName { get; set; }

    // Content
    public string Subject { get; set; }
    public string Body { get; set; }
}