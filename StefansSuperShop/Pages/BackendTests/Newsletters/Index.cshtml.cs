using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Services;
using StefansSuperShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace StefansSuperShop.Pages.BackendTests.Newsletters
{
    public class IndexModel : PageModel
    {
        private readonly INewsletterService _newsletterService;

        public IndexModel(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        public IList<Newsletter> Newsletters { get;set; }
        public IList<NewsletterJoin> JoinedList { get; set; }

        public async Task OnGetAsync()
        {
            var newsletters = await _newsletterService.GetAll();
            var onlyUnsent = newsletters.Where(n => n.SendDate == DateTime.MinValue);
            Newsletters = onlyUnsent.ToList();

            var newslettersSent = await _newsletterService.GetAllSent();

            var results = newsletters.Join(newslettersSent,
                        sent => sent.NewsletterId,
                        unsent => unsent.NewsletterId,
                        (unsent, sent) => new
                        {
                            NEWSLETTERID = unsent.NewsletterId,
                            NEWSLETTERTITLE = unsent.Title,
                            NEWSLETTERCONTENT = unsent.Content,
                            NEWSLETTERSENDDATE = unsent.SendDate,
                            NEWSLETTERSENTID = sent.NewsletterSentId,
                            NEWSLETTERRECIPIENTID = sent.ApplicationUserId,
                            NEWSLETTERRECIPIENTEMAIL = sent.ApplicationUserEmail
                        })
                         .Select(s => new NewsletterJoin
                         {
                             NewsletterId = s.NEWSLETTERID,
                             Title = s.NEWSLETTERTITLE,
                             Content = s.NEWSLETTERCONTENT,
                             NewsletterSentId = s.NEWSLETTERSENTID,
                             SendDate = s.NEWSLETTERSENDDATE,
                             RecipientId = s.NEWSLETTERRECIPIENTID,
                             RecipientEmail = s.NEWSLETTERRECIPIENTEMAIL
                         }).ToList();

            JoinedList = results;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _newsletterService.CreateSentNewsletter(id);
            return Page();
        }
    }
}
