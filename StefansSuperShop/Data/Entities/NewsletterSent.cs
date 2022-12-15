using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StefansSuperShop.Data.Entities
{
    public class NewsletterSent
    {
        [Key]
        [Column("NewsletterSentID")]
        public int NewsletterSentId { get; set; }
        public int NewsletterId { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
