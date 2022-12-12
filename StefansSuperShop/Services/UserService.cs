using Microsoft.AspNetCore.Identity;
using StefansSuperShop.Repositories;

namespace StefansSuperShop.Services
{
    interface IUserService
    {
        public void RegisterUser(string email, string password);
        public void RegisterNewsletterOnly(string email);
        public void RegisterUpgradeFromNewsletter(string email, string password);
        public void DeleteUser(string email);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void RegisterUser(string email, string password)
        {
            if (!CheckEmail(email))
            {
                throw new System.Exception("Email is already in use");
            }

            var user = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var role = "Customer";

            _userRepository.RegisterUser(user, password, role);
        }

        public void RegisterNewsletterOnly(string email)
        {
            if (!CheckEmail(email))
            {
                throw new System.Exception("Email is already in use");
            }

            var user = new IdentityUser
            {
                Email = email,
                EmailConfirmed = true
                // lägg till NewsletterActive = true när vi mergat in från andra feature branchen
            };

            _userRepository.RegisterUser(user);
        }

        public void RegisterUpgradeFromNewsletter(string email, string password)
        {

        }

        public void DeleteUser(string email)
        {
            var user = _userRepository.GetUser(email);

            if (user != null)
            {
                _userRepository.DeleteUser(user);
            }
        }

        private bool CheckEmail(string email)
        {
            return _userRepository.GetUser(email) != null;
        }
    }
}
