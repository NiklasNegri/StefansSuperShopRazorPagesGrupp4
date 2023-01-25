using System.Collections.Generic;
using System.ComponentModel;

namespace StefansSuperShop.Data.Model;

public class MailData
{
    // Receiver
    public List<string> To { get; set; } = new List<string>();
    public List<string> Cc { get; set; } = new List<string>();
    public List<string> Bcc { get; set; } = new List<string>();

    // Sender
    [DisplayName("Email")]
    public string From { get; set; }

    [DisplayName("Name")]
    public string DisplayName { get; set; }
    public string ReplyTo { get; set; }
    public string ReplyToName { get; set; }

    // Content
    public string Subject { get; set; }
    public string Body { get; set; }
}