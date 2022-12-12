using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StefansSuperShop.Data
{
    public class Newsletters
    {
        [Key]
        [Column("NewsletterID")]
        public int NewsletterId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool HasBeenSent { get; set; }
    }
}
