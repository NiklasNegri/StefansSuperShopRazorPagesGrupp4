using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StefansSuperShop.Data.DTOs;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Data.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StefansSuperShop.Repositories
{
    public interface IUserRepository
    {
        public Task RegisterUser(ApplicationUserDTO model);
        public Task RegisterNewsletterUser(ApplicationUserDTO model);
        public Task RegisterUpgradeFromNewsletter(ApplicationUserDTO model);
        public Task<ApplicationUser> GetById(string id);
        public Task<ApplicationUser> GetByEmail(string email);
        public Task<IEnumerable<ApplicationUser>> GetAll();
        public Task<IList<string>> GetUserRoles(string id);
        public Task<IList<ApplicationUserDTO>> GetAllUsersAndRoles();
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
            user.UserName = model.Email;
            user.Email = model.Email;
            user.NewsletterIsActive = false;

            // TODO EmailConfirmed should not be set as true as standard for registring users
            user.EmailConfirmed = true;

            await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRolesAsync(user, model.Roles);

        }

        public async Task RegisterNewsletterUser(ApplicationUserDTO model)
        {
            var user = _mapper.Map<ApplicationUser>(model);
            user.Id = Guid.NewGuid().ToString();
            user.UserName = model.Email;
            user.Email = model.Email;
            

            // TODO EmailConfirmed should not be set as true as standard for registring users
            user.EmailConfirmed = true;

            user.NewsletterIsActive = true;
            await _userManager.CreateAsync(user);
            await _userManager.AddToRoleAsync(user, "Customer");
        }
        
        public async Task RegisterUpgradeFromNewsletter(ApplicationUserDTO model)
        {
            var user = await GetById(model.Id);
            await _userManager.AddPasswordAsync(user, model.Password);
            await _userManager.AddToRolesAsync(user, model.Roles);
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<ApplicationUser> GetByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            return await _context.ApplicationUsers.ToListAsync();
        }

        public async Task<IList<ApplicationUserDTO>> GetAllUsersAndRoles()
        {
            var list = await (from user in _context.ApplicationUsers
                              join userRoles in _context.UserRoles on user.Id equals userRoles.UserId
                              join role in _context.Roles on userRoles.RoleId equals role.Id
                              select new ApplicationUserDTO
                              {
                                  Id = user.Id,
                                  UserName = user.UserName,
                                  NewsletterIsActive = user.NewsletterIsActive,
                                  Roles = (from r in _context.Roles
                                           join ur in _context.UserRoles on r.Id equals ur.RoleId
                                           where ur.UserId == user.Id
                                           select r.Name).ToArray()
                              })
                           .ToListAsync();

            return list.DistinctBy(u => u.Id).ToList();
        }

        public async Task<IList<string>> GetUserRoles(string id)
        {
            var user = await GetById(id);
            return await _userManager.GetRolesAsync(user);
        }

        public async Task UpdateEmail(ApplicationUserDTO model)
        {
            var user = await GetById(model.Id);
            user.UserName = model.Email;
            var token = _userManager.GenerateChangeEmailTokenAsync(user, model.Email).ToString();
            await _userManager.ChangeEmailAsync(user, model.Email, token);
            await _userManager.UpdateNormalizedEmailAsync(user);
            await _userManager.UpdateAsync(user);
        }

        public async Task UpdatePassword(ApplicationUserDTO model)
        {
            var user = await GetById(model.Id);
            await _userManager.ChangePasswordAsync(user, model.Password, model.Password);
        }

        public async Task UpdateNewsletterActive(ApplicationUserDTO model)
        {
            var user = await GetById(model.Id);
            user.NewsletterIsActive = model.NewsletterIsActive;
            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteUser(string id)
        {
            var user = await GetById(id);
            await _userManager.DeleteAsync(user);
        }
    }
}
