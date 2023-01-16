using StefansSuperShop.Data.DTOs;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StefansSuperShop.Services
{
    public interface IUserService
    {
        public Task RegisterUser(ApplicationUserDTO model);
        public Task RegisterNewsletterUser(ApplicationUserDTO model);
        public Task<ApplicationUser> GetById(string id);
        public Task<ApplicationUser> GetByEmail(string email);
        public Task<IList<string>> GetUserRoles(string id);
        public Task<IEnumerable<ApplicationUser>> GetAll();
        public Task<IList<ApplicationUserDTO>> GetAllUsersAndRoles();
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
            var users = await GetAll();
            if (users.Any(u => u.Email == model.Email))
            {
                throw new Exception("User with that email is already registered!");
            }
            await _userRepository.RegisterUser(model);
        }

        public async Task RegisterNewsletterUser(ApplicationUserDTO model)
        {
            var users = await GetAll();
            if (users.Any(u => u.Email == model.Email))
            {
                throw new Exception("User with that email is already registered!");
            }
            await _userRepository.RegisterNewsletterUser(model);
        }

        public Task<ApplicationUser> GetById(string id) => 
            _userRepository.GetById(id) ?? throw new Exception("User does not exist!");

        public Task<ApplicationUser> GetByEmail(string email) =>
            _userRepository.GetByEmail(email) ?? throw new Exception("User with that email does not exist!");

        public Task<IEnumerable<ApplicationUser>> GetAll() => 
            _userRepository.GetAll() ?? throw new Exception("No users found!");

        public Task<IList<string>> GetUserRoles(string id) =>
            _userRepository.GetUserRoles(id) ?? throw new Exception("No users found!");

        public Task<IList<ApplicationUserDTO>> GetAllUsersAndRoles() =>
            _userRepository.GetAllUsersAndRoles() ?? throw new Exception("No users found!");

        public async Task UpdateUser(ApplicationUserDTO model)
        {
            if (GetById(model.Id) == null)
            {
                throw new Exception("User does not exist!");
            }

            if (model.Email != null)
            {
                await _userRepository.UpdateEmail(model);
            }
            else if (model.Password != null)
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
            var user = await GetById(id);
            if (user == null)
            {
                throw new Exception("User does not exist!");
            }
            await _userRepository.DeleteUser(id);
        }
    }
}
