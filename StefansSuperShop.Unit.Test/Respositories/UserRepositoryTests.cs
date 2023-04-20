using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using StefansSuperShop.Data.DTOs;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Data.Helpers;
using StefansSuperShop.Repositories;

namespace StefansSuperShop.Unit.Test.Respositories;

public class UserRepositoryTests :IDisposable
{
    private Mock<IMapper> _mapperMock;
    private ApplicationDbContext _context;
    private Mock<UserManager<ApplicationUser>> _userManagerMock;

    private UserRepository _sut;
    public UserRepositoryTests()
    {
        _mapperMock = new Mock<IMapper>();

        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var contextOption = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(connection)
            .Options;
        _context = new ApplicationDbContext(contextOption);
        _context.Database.EnsureCreated();

        var store = new Mock<IUserStore<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
        _userManagerMock.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
        _userManagerMock.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

        _sut = new UserRepository(_userManagerMock.Object, _context, _mapperMock.Object);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public async void RegisterUser()
    {
        _mapperMock.Setup(x => x.Map<ApplicationUser>(It.IsAny<ApplicationUserDTO>()))
            .Returns(new ApplicationUser
        {
            Email = "test@test.se",
            UserName = "test@test.se",
            NewsletterIsActive = false,
        });

        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(new IdentityResult());
        _userManagerMock.Setup(x => x.AddToRolesAsync(It.IsAny<ApplicationUser>(), It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(new IdentityResult());

        var appUser = new ApplicationUserDTO()
        {
            Email = "test@test.se"
        };

        await _sut.RegisterUser(appUser);
    }
}
