using System;

namespace StefansSuperShop.ViewModels
{
    public class NewsletterJoin
    {
        public int NewsletterId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime SendDate { get; set; }
        public int NewsletterSentId { get; set; }
        public string RecipientId { get; set; }
        public string RecipientEmail { get; set; }
    }
}
