using StefansSuperShop.Data.Entities;
using StefansSuperShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace StefansSuperShop.Services
{
    public interface INewsletterService
    {
        public Task CreateNewsletter(string title, string content);
        public Task CreateSentNewsletter(int id);
        public Task<Newsletter> GetById(int id);
        public Task<NewsletterSent> GetByIdSent(int id);
        public Task<IEnumerable<Newsletter>> GetAll();
        public Task<IEnumerable<NewsletterSent>> GetAllSent();
        public Task<IEnumerable<ApplicationUser>> GetAllRecipients(int newsletterId);
        public Task EditNewsletter(int id, string title = null, string content = null);
        public Task DeleteNewsletter(int id);
    }

    public class NewsletterService : INewsletterService
    {
        private readonly INewsletterRepository _newsletterRepository;
        private readonly IUserRepository _userRepository;

        public NewsletterService(INewsletterRepository newsletterRepository, IUserRepository userRepository)
        {
            _newsletterRepository = newsletterRepository;
            _userRepository = userRepository;
        }

        public async Task CreateNewsletter(string title, string content)
        {
            var newsletter = new Newsletter { Title = title, Content = content };
            await _newsletterRepository.CreateNewsletter(newsletter);
        }

        public async Task CreateSentNewsletter(int id)
        {
            var newsletter = await _newsletterRepository.GetById(id);

            if (newsletter == null)
            {
                throw new Exception("Newsletter with that id does not exist");
            }

            var users = await _userRepository.GetAll();
            var recipients = users.Where(u => u.NewsletterIsActive == true);
            var newslettersSent = new List<NewsletterSent>();

            foreach (var recipient in recipients)
            {
                newslettersSent.Add(new NewsletterSent
                {
                    NewsletterId = newsletter.NewsletterId,
                    ApplicationUserId = recipient.Id,
                    ApplicationUserEmail = recipient.Email,
                });
            }

            newsletter.SendDate = DateTime.Now;

            await _newsletterRepository.CreateSentNewsletter(newsletter, newslettersSent);
        }

        public async Task<Newsletter> GetById(int id) => await _newsletterRepository.GetById(id);

        public async Task<NewsletterSent> GetByIdSent(int id) => await _newsletterRepository.GetByIdSent(id);

        public async Task<IEnumerable<Newsletter>> GetAll() => await _newsletterRepository.GetAll();

        public async Task<IEnumerable<NewsletterSent>> GetAllSent() => await _newsletterRepository.GetAllSent();

        public async Task<IEnumerable<ApplicationUser>> GetAllRecipients(int newsletterId)
        {
            var recipients = new List<ApplicationUser>();
            var newslettersSent = await GetAllSent();

            foreach (var newsletter in newslettersSent.Where(n => n.NewsletterId == newsletterId))
            {
                if (newsletter == null)
                {
                    throw new Exception("No sent newsletter with that id");
                }
                var user = await _userRepository.GetById(newsletter.ApplicationUserId);
                recipients.Add(user);
            }
            return recipients;
        }

        public async Task EditNewsletter(int id, string title, string content)
        {
            var newsletter = await CheckNewsletterExists(id);

            newsletter.Title = title ?? newsletter.Title;
            newsletter.Content = content ?? newsletter.Content;

            await _newsletterRepository.EditNewsletter(newsletter);
        }

        public async Task DeleteNewsletter(int id)
        {
            var newsletter = await CheckNewsletterExists(id);

            await _newsletterRepository.DeleteNewsletter(newsletter);
        }

        private async Task<Newsletter> CheckNewsletterExists(int id)
        {
            var newsletter = await _newsletterRepository.GetById(id);

            if (newsletter == null)
            {
                throw new Exception("Newsletter with that ID does not exist");
            }

            return newsletter;
        }
    }
}
