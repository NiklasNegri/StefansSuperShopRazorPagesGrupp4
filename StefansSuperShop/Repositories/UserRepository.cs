using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StefansSuperShop.Data.DTOs;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StefansSuperShop.Repositories
{
    public interface IUserRepository
    {
        public Task RegisterUser(ApplicationUserDTO model);
        public Task RegisterUpgradeFromNewsletter(ApplicationUserDTO model);
        public Task<ApplicationUser> GetById(string id);
        public Task<IEnumerable<ApplicationUser>> GetAll();
        public Task UpdateEmail(ApplicationUserDTO model);
        public Task UpdatePassword(ApplicationUserDTO model);
        public Task UpdateNewsletterActive(ApplicationUserDTO model);
        public Task DeleteUser(string id);
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;   

        public UserRepository(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        public async Task RegisterUser(ApplicationUserDTO model)
        {
            var user = _mapper.Map<ApplicationUser>(model);
            user.Id = Guid.NewGuid().ToString();
            user.Email = model.NewEmail;

            if (model.NewPassword == null)
            {
                user.NewsletterActive = true;
                await _userManager.CreateAsync(user);
                return;
            }

            await _userManager.CreateAsync(user, model.NewPassword);
        }
        
        public async Task RegisterUpgradeFromNewsletter(ApplicationUserDTO model)
        {
            var user = await GetById(model.Id);
            await _userManager.AddPasswordAsync(user, model.NewPassword);
            await _userManager.AddToRoleAsync(user, model.Role);
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            return await _context.ApplicationUsers.FindAsync(id);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            return await _context.ApplicationUsers.ToListAsync();
        }

        public async Task UpdateEmail(ApplicationUserDTO model)
        {
            var user = await GetById(model.Id);
            user.UserName = model.NewEmail;
            var token = _userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail).ToString();
            await _userManager.ChangeEmailAsync(user, model.NewEmail, token);
            await _userManager.UpdateNormalizedEmailAsync(user);
            await _userManager.UpdateAsync(user);
        }

        public async Task UpdatePassword(ApplicationUserDTO model)
        {
            var user = await GetById(model.Id);
            await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }

        public async Task UpdateNewsletterActive(ApplicationUserDTO model)
        {
            var user = await GetById(model.Id);
            user.NewsletterActive = model.NewsletterActive;
            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteUser(string id)
        {
            var user = await GetById(id);
            await _userManager.DeleteAsync(user);
        }
    }
}
