using Microsoft.AspNetCore.Identity;

namespace StefansSuperShop.Repositories
{
    public interface IUserRepository
    {
        public IdentityUser GetUser(string email);
        public void RegisterUser(IdentityUser user, string password = null, string role = null);
        public void DeleteUser(IdentityUser user);
        public void UpdateUser(IdentityUser user);

    }
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IdentityUser GetUser(string email)
        {
            return _userManager.FindByEmailAsync(email).Result;
        }

        public void RegisterUser(IdentityUser user, string password, string role)
        {
            if (password != null)
            {
                _userManager.CreateAsync(user, password);
                _userManager.AddToRoleAsync(user, role);
            }
            else
            {
                _userManager.CreateAsync(user);
            }
        }

        public void DeleteUser(IdentityUser user)
        {
            _userManager.DeleteAsync(user);
        }

        public void UpdateUser(IdentityUser user)
        {
            _userManager.UpdateAsync(user);
        }
    }
}
