using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StefansSuperShop.Data.Entities
{
    public class NewsletterSent
    {
        [Key]
        [Column("NewsletterID")]
        public int NewsletterId { get; set; }
        public int AspNetUserId { get; set; }
        public DateTime SendDate { get; set; }
    }
}
