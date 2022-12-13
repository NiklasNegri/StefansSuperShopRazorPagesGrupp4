using StefansSuperShop.Data.Entities;
using StefansSuperShop.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace StefansSuperShop.Services
{
    public interface IUserService
    {
        public void RegisterUser(string email, string password, string role);
        public void RegisterNewsletterOnly(string email);
        public ApplicationUser GetByEmail(string email);
        public IEnumerable<ApplicationUser> GetAll();
        public void UpdateUser(string id, string email = null, string password = null);
        public void UpdateUserFromNewsletter(string email, string password);
        public void ToggleNewsletter(string email, bool state);
        public void DeleteUser(string email);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void RegisterUser(string email, string password, string role)
        {
            if (!EmailInUse(email))
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    NewsletterActive = false
                };

                _userRepository.RegisterUser(user, password, role);
            }
        }

        public void RegisterNewsletterOnly(string email)
        {
            if (!EmailInUse(email))
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    NewsletterActive = true
                };

                _userRepository.RegisterUser(user);
            }           
        }

        public ApplicationUser GetByEmail(string email) => _userRepository.GetById(email);

        public IEnumerable<ApplicationUser> GetAll() => _userRepository.GetAll();

        public void UpdateUser(string id, string email, string password)
        {
            if (UserNotExist(id))
            {
                var user = _userRepository.GetById(id);

                if (email != null && !EmailInUse(email))
                {
                    user.UserName = email;
                    user.Email = email;
                    _userRepository.UpdateEmail(user, email);
                }
            }
        }

        public void UpdatePassword(string id, string oldPassword, string newPassword)
        { 
            if (!UserNotExist(id))
            {
                var user = _userRepository.GetById(id);
                _userRepository.UpdatePassword(user, oldPassword, newPassword);
            }
        }

        public void UpdateUserFromNewsletter(string id, string password)
        {
            if (!UserNotExist(id))
            {
                var user = _userRepository.GetById(id);
                var role = "Customer";
                _userRepository.RegisterUpgradeFromNewsletter(user, password, role);
            }
        }

        public void ToggleNewsletter(string id, bool state)
        {
            if (!UserNotExist(id))
            {
                var user = _userRepository.GetById(id);
                user.NewsletterActive = state;
                _userRepository.UpdateUser(user);
            }
        }

        public void DeleteUser(string id)
        {
            if (!UserNotExist(id))
            {
                var user = _userRepository.GetById(id);
                _userRepository.DeleteUser(user);
            }
        }

        private bool EmailInUse(string email)
        {
            if (_userRepository.GetAll().Any(u => u.Email == email))
            {
                return true;
                throw new System.Exception("Email is already in use");
            }
            return false;
        }

        private bool UserNotExist(string id)
        {
            if (_userRepository.GetAll().Any(u => u.Id != id))
            {
                return true;
                throw new System.Exception("User with that email does not exist");
            }
            return false;
        }
    }
}
