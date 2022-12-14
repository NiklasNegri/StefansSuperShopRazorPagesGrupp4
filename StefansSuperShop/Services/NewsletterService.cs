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
        public Task<IEnumerable<Newsletter>> GetAll();
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

            var recipients = await _userRepository.GetAll();
            var newslettersSent = new List<NewsletterSent>();

            var newsletterSent = recipients.Select(
                recipient => new NewsletterSent()
                {
                    NewsletterId = newsletter.NewsletterId,
                    ApplicationUserId = Int32.Parse(recipient.Id)
                });

            newsletter.SendDate = DateTime.Now;

            await _newsletterRepository.CreateSentNewsletter(newsletter, newslettersSent);
        }

        public async Task<Newsletter> GetById(int id) => await _newsletterRepository.GetById(id);

        public async Task<IEnumerable<Newsletter>> GetAll() => await _newsletterRepository.GetAll();

        public async Task EditNewsletter(int id, string title, string content)
        {
            var newsletter = await CheckNewsletterExists(id);

            if (title != null) newsletter.Title = title;
            if (content != null) newsletter.Content = content;

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
