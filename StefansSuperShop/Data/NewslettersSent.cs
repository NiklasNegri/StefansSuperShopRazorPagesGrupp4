using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StefansSuperShop.Data
{
    public class NewslettersSent
    {
        [Key]
        [Column("NewsletterID")]
        public int NewsletterId { get; set; }
        public int AspNetUserId { get; set; }
        public DateTime SendDate { get; set; }
    }
}
