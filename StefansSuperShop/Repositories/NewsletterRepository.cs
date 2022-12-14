using Microsoft.EntityFrameworkCore;
using StefansSuperShop.Data.Entities;
using System.Collections.Generic;
using StefansSuperShop.Data.Helpers;

namespace StefansSuperShop.Repositories
{
    public interface INewsletterRepository
    {
        public void CreateNewsletter(Newsletter newsletter);
        public void CreateSentNewsletter(Newsletter newsletter, List<NewsletterSent> newslettersSent);
        public Newsletter GetById(int id);
        public IEnumerable<Newsletter> GetAll();
        public void EditNewsletter(Newsletter newsletter);
        public void DeleteNewsletter(Newsletter newsletter);
    }

    public class NewsletterRepository : INewsletterRepository
    {
        private readonly ApplicationDbContext _context;

        public NewsletterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateNewsletter(Newsletter newsletter)
        {
            _context.Newsletters.Add(newsletter);
            _context.SaveChanges();
        }

        public void CreateSentNewsletter(Newsletter newsletter, List<NewsletterSent> newslettersSent)
        {
            foreach (var newsletterSent in newslettersSent)
            {
                _context.NewslettersSent.Add(newsletterSent);
            }
            EditNewsletter(newsletter);
            _context.SaveChanges();
        }

        public Newsletter GetById(int id)
        {
            return _context.Newsletters.Find(id);
        }

        public IEnumerable<Newsletter> GetAll()
        {
            return _context.Newsletters;
        }

        public void EditNewsletter(Newsletter newsletter)
        {
            _context.Newsletters.Update(newsletter);
            _context.SaveChanges();
        }

        public void DeleteNewsletter(Newsletter newsletter)
        {
            _context.Newsletters.Remove(newsletter);
            _context.SaveChanges();
        }
    }
}
