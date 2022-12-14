using AutoMapper;
using StefansSuperShop.Data.DTOs;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StefansSuperShop.Services
{
    public interface IUserService
    {
        public Task RegisterUser(ApplicationUserDTO model);
        public ApplicationUser GetById(string email);
        public IEnumerable<ApplicationUser> GetAll();
        public Task UpdateUser(ApplicationUserDTO model);
        public Task UpdateUserFromNewsletter(ApplicationUserDTO model);
        public Task UpdateNewsletterActive(ApplicationUserDTO model);
        public Task DeleteUser(string id);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterUser(ApplicationUserDTO model)
        {
            if (GetAll().Any(u => u.Email == model.NewEmail))
            {
                throw new System.Exception("User with that email is already registered!");
            }
            await _userRepository.RegisterUser(model);
        }

        public ApplicationUser GetById(string id) => _userRepository.GetById(id);

        public IEnumerable<ApplicationUser> GetAll() => _userRepository.GetAll();


        public async Task UpdateUser(ApplicationUserDTO model)
        {
            if (GetById(model.Id) == null)
            {
                throw new System.Exception("User does not exist!");
            }

            if (model.NewEmail != null)
            {
                await _userRepository.UpdateEmail(model);
            }
            else if (model.NewPassword != null)
            {
                await _userRepository.UpdatePassword(model);
            }
        }

        public async Task UpdateUserFromNewsletter(ApplicationUserDTO model)
        {
            await _userRepository.RegisterUpgradeFromNewsletter(model);
        }

        public async Task UpdateNewsletterActive(ApplicationUserDTO model)
        {
            
            await _userRepository.UpdateNewsletterActive(model);
        }

        public async Task DeleteUser(string id)
        {
            if (GetById(id) == null)
            {
                throw new System.Exception("User does not exist!");
            }
            await _userRepository.DeleteUser(id);
        }
    }
}
