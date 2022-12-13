using StefansSuperShop.Data.Entities;
using StefansSuperShop.Repositories;

namespace StefansSuperShop.Services
{
    public class NewsletterService
    {
        private readonly INewsletterRepository _newsletterRepository;

        public NewsletterService(INewsletterRepository newsletterRepository)
        {
            _newsletterRepository = newsletterRepository;
        }

        public void CreateNewsletter(string title, string content)
        {
            var newsletter = new Newsletter { Title = title, Content = content };
            _newsletterRepository.CreateNewsletter(newsletter);
        }

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
