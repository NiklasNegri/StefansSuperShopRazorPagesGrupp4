using System.Collections.Generic;

namespace StefansSuperShop.Model;

public class MailData
{
	// Receiver
	public List<string> To { get; }
	public List<string> Cc { get; }
	public List<string> Bcc { get; }

	// Sender
	public string From { get; }
	public string DisplayName { get; }
	public string ReplyTo { get; }
	public string ReplyToName { get; }

	// Content
	public string Subject { get; }
	public string Body { get; }

	//public MailData(List<string> to, string subject, string body = null, string from = null, string displayName = null, string replyTo = null, string replyToName = null, List<string> bcc = null, List<string> cc = null)
	//{
	//	// Receiver
	//	To = to ?? new List<string>();
	//	Bcc = bcc ?? new List<string>();
	//	Cc = cc ?? new List<string>();

	//	// Sender
	//	From = from;
	//	DisplayName = displayName;
	//	ReplyTo = replyTo;
	//	ReplyToName = replyToName;

	//	// Content
	//	Subject = subject;
	//	Body = body;
	//}
}