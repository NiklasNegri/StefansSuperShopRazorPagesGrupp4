using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StefansSuperShop.Data.Entities
{
    public class Newsletter
    {
        [Key]
        [Column("NewsletterID")]
        public int NewsletterId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime SendDate { get; set; }
    }
}
