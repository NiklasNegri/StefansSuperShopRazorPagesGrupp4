using Microsoft.AspNetCore.Identity;
using StefansSuperShop.Data;
using StefansSuperShop.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StefansSuperShop.Repositories
{
    public interface IUserRepository
    {
        public void RegisterUser(ApplicationUser user, string password = null, string role = null);
        public void RegisterUpgradeFromNewsletter(ApplicationUser user, string password, string role);
        public ApplicationUser GetById(string id);
        public IEnumerable<ApplicationUser> GetAll();
        public void UpdateUser(ApplicationUser user, string password = null);
        public void UpdateEmail(ApplicationUser user, string email);
        public void UpdatePassword(ApplicationUser user, string oldPassword, string newPassword);
        public void DeleteUser(ApplicationUser user);

    }
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserRepository(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public void RegisterUser(ApplicationUser user, string password, string role)
        {
            if (password != null)
            {
                var result = _userManager.CreateAsync(user, password).Result;
                var r = _userManager.AddToRoleAsync(user, role).Result;
            }
            else
            {
                var result = _userManager.CreateAsync(user).Result;
            }
        }
        
        public void RegisterUpgradeFromNewsletter(ApplicationUser user, string password, string role)
        {
            _userManager.AddPasswordAsync(user, password);
            _userManager.AddToRoleAsync(user, role);
        }

        public ApplicationUser GetById(string id)
        {
            return _context.ApplicationUsers.Find(id);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.ApplicationUsers;
        }

        public void UpdateUser(ApplicationUser user, string password)
        {
            if (password != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);
            }
            _userManager.UpdateAsync(user);
        }

        public void UpdateEmail(ApplicationUser user, string email)
        {
            var emailConfirmationToken = _userManager.GenerateChangeEmailTokenAsync(user, email).ToString();
            var result = _userManager.ChangeEmailAsync(user, email, emailConfirmationToken);
        }

        public void UpdatePassword(ApplicationUser user, string oldPassword, string newPassword)
        {
            _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public void DeleteUser(ApplicationUser user)
        {
            _userManager.DeleteAsync(user);
        }
    }
}
