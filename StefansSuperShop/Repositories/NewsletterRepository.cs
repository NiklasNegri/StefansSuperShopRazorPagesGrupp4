using Microsoft.EntityFrameworkCore;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Data.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StefansSuperShop.Repositories
{
    public interface INewsletterRepository
    {
        public Task CreateNewsletter(Newsletter newsletter);
        public Task CreateSentNewsletter(Newsletter newsletter, List<NewsletterSent> newslettersSent);
        public Task<Newsletter> GetById(int id);
        public Task<IEnumerable<Newsletter>> GetAll();
        public Task EditNewsletter(Newsletter newsletter);
        public Task DeleteNewsletter(Newsletter newsletter);
    }

    public class NewsletterRepository : INewsletterRepository
    {
        private readonly ApplicationDbContext _context;

        public NewsletterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateNewsletter(Newsletter newsletter)
        {
            await _context.Newsletters.AddAsync(newsletter);
            await _context.SaveChangesAsync();
        }

        public async Task CreateSentNewsletter(Newsletter newsletter, List<NewsletterSent> newslettersSent)
        {
            foreach (var newsletterSent in newslettersSent)
            {
                await _context.NewslettersSent.AddAsync(newsletterSent);
            }
            await EditNewsletter(newsletter);
            await _context.SaveChangesAsync();
        }

        public async Task<Newsletter> GetById(int id)
        {
            return await _context.Newsletters.FindAsync(id);
        }

        public async Task<IEnumerable<Newsletter>> GetAll()
        {
            return await _context.Newsletters.ToListAsync();
        }

        public async Task EditNewsletter(Newsletter newsletter)
        {
            _context.Newsletters.Update(newsletter);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNewsletter(Newsletter newsletter)
        {
            _context.Newsletters.Remove(newsletter);
            await _context.SaveChangesAsync();
        }
    }
}
