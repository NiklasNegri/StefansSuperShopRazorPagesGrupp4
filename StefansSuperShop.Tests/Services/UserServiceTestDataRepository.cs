using StefansSuperShop.Data.DTOs;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Services;

namespace StefansSuperShop.Test.Services;

public class UserServiceTestDataRepository : IUserService
{
    public Task RegisterUser(ApplicationUserDTO model)
    {
        return Task.CompletedTask;
    }

    public Task<ApplicationUser> GetById(string id)
    {
        return null;
    }

    public Task<ApplicationUser> GetByEmail(string email)
    {
        return null;
    }

    public Task<IEnumerable<ApplicationUser>> GetAll()
    {
        return null;
    }

    public Task UpdateUser(ApplicationUserDTO model)
    {
        return Task.CompletedTask;
    }

    public Task UpdateUserFromNewsletter(ApplicationUserDTO model)
    {
        return Task.CompletedTask;
    }

    public Task UpdateNewsletterActive(ApplicationUserDTO model)
    {
        return Task.CompletedTask;
    }

    public Task DeleteUser(string id)
    {
        return Task.CompletedTask;
    }
}
