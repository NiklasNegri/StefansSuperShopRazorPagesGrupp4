using StefansSuperShop.Data.Entities;
using StefansSuperShop.Repositories;
using System;
using System.Collections.Generic;

namespace StefansSuperShop.Services
{
    public interface INewsletterService
    {
        public void CreateNewsletter(string title, string content);
        public void CreateSentNewsletter(int id);
        public Newsletter GetById(int id);
        public IEnumerable<Newsletter> GetAll();
        public void EditNewsletter(int id, string title = null, string content = null);
        public void DeleteNewsletter(int id);
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

        public void CreateNewsletter(string title, string content)
        {
            var newsletter = new Newsletter { Title = title, Content = content };
            _newsletterRepository.CreateNewsletter(newsletter);
        }

        public void CreateSentNewsletter(int id)
        {
            var newsletter = _newsletterRepository.GetById(id);

            if (newsletter == null)
            {
                throw new System.Exception("Newsletter with that id does not exist");
            }

            var recipients = _userRepository.GetAll();
            var newslettersSent = new List<NewsletterSent>();

            foreach (var recipient in recipients)
            {
                var newsletterSent = new NewsletterSent()
                {
                    NewsletterId = newsletter.NewsletterId,
                    ApplicationUserId = Int32.Parse(recipient.Id)
                };
                newslettersSent.Add(newsletterSent);
            }

            newsletter.SendDate = DateTime.Now;

            _newsletterRepository.CreateSentNewsletter(newsletter, newslettersSent);
        }

        public Newsletter GetById(int id) => _newsletterRepository.GetById(id);

        public IEnumerable<Newsletter> GetAll() => _newsletterRepository.GetAll();

        public void EditNewsletter(int id, string title, string content)
        {
            var newsletter = CheckNewsletterExists(id);

            if (title != null) newsletter.Title = title;
            if (content != null) newsletter.Content = content;

            _newsletterRepository.EditNewsletter(newsletter);
        }

        public void DeleteNewsletter(int id)
        {
            var newsletter = CheckNewsletterExists(id);

            _newsletterRepository.DeleteNewsletter(newsletter);
        }

        private Newsletter CheckNewsletterExists(int id)
        {
            var newsletter = _newsletterRepository.GetById(id);

            if (newsletter == null)
            {
                throw new System.Exception("Newsletter with that ID does not exist");
            }

            return newsletter;
        }
    }
}
