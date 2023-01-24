using AutoFixture;
using Castle.Components.DictionaryAdapter.Xml;
using Moq;
using StefansSuperShop.Data.DTOs;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Repositories;
using StefansSuperShop.Services;

namespace StefansSuperShop.Unit.Test.Services;

public class UserServiceTests
{
    private Mock<IUserRepository> _userRepositoryMock;
	private Fixture _fixture = new();
	private UserService _sut;

	public UserServiceTests()
	{
		_userRepositoryMock = new Mock<IUserRepository>();
		_sut = new UserService( _userRepositoryMock.Object );
	}

	[Fact]
	public async void RegisterUser_ValidInput_ThrowsNoException()
	{
        ApplicationUserDTO user = _fixture.Create<ApplicationUserDTO>();

		await _sut.RegisterUser( user );
    }

    [Fact]
    public async Task RegisterUser_InValidInput_ThrowsException()
    {
		_userRepositoryMock.Setup(x => x.GetAll())
			.ReturnsAsync(new List<ApplicationUser>()
			{
				new ApplicationUser { Email = "test@test.se"}
			});

        ApplicationUserDTO user = _fixture.Create<ApplicationUserDTO>();
		user.Email = "test@test.se";

        await Assert.ThrowsAsync<Exception>(() => _sut.RegisterUser(user));
    }

    [Fact]
    public async void RegisterNewsletterUser_ValidInput_ThrowsNoException()
    {
        ApplicationUserDTO user = _fixture.Create<ApplicationUserDTO>();

        await _sut.RegisterNewsletterUser(user);
    }

    [Fact]
    public async Task RegisterNewsletterUser_InValidInput_ThrowsException()
    {
        _userRepositoryMock.Setup(x => x.GetAll())
            .ReturnsAsync(new List<ApplicationUser>()
            {
                new ApplicationUser { Email = "test@test.se"}
            });

        ApplicationUserDTO user = _fixture.Create<ApplicationUserDTO>();
        user.Email = "test@test.se";

        await Assert.ThrowsAsync<Exception>(() => _sut.RegisterNewsletterUser(user));
    }

    [Fact]
    public async Task GetById_UserExist_ReturnsUser()
    {
        string expected = "1";
        _userRepositoryMock.Setup(x => x.GetById(expected))
            .ReturnsAsync(new ApplicationUser()
            {
                Id = expected,
            });

        var user = await _sut.GetById("1");
        Assert.Equal(expected, user.Id);
    }

    [Fact]
    public async Task GetById_UserDoesNotExist_ThrowsException()
    {
        _userRepositoryMock.Setup(x => x.GetById("1"))
            .ThrowsAsync(new Exception("User does not exist!"));

        await Assert.ThrowsAsync<Exception>(() => _sut.GetById("1"));
    }

    [Fact]
    public async Task GetByEmail_UserExist_ReturnsUser()
    {
        string expected = "1";
        _userRepositoryMock.Setup(x => x.GetByEmail("test@test.se"))
            .ReturnsAsync(new ApplicationUser()
            {
                Id = expected,
                Email = "test@test.se"
            });

        var user = await _sut.GetByEmail("test@test.se");
        Assert.Equal(expected, user.Id);
    }

    [Fact]
    public async Task GetByEmail_UserDoesNotExist_ThrowsException()
    {
        _userRepositoryMock.Setup(x => x.GetByEmail("test@test.se"))
            .ThrowsAsync(new Exception("User does not exist!"));

        await Assert.ThrowsAsync<Exception>(() => _sut.GetByEmail("test@test.se"));
    }

    [Fact]
    public async Task GetAll_UserExists_ReturnsUsers()
    {
        string expected = "1";
        _userRepositoryMock.Setup(x => x.GetAll())
            .ReturnsAsync(new List<ApplicationUser>(){
                new ApplicationUser()
                {
                    Id = expected,
                    Email = "test@test.se"
                }
            });

        var users = await _sut.GetAll();
        Assert.Equal(expected, users.ToList()[0].Id);
    }

    [Fact]
    public async Task GetAll_UserListEmpty_ThrowsException()
    {
        _userRepositoryMock.Setup(x => x.GetAll())
            .ThrowsAsync(new Exception("No users found!"));

        await Assert.ThrowsAsync<Exception>(() => _sut.GetAll());
    }

    [Fact]
    public async Task GetUserRoles_UserRoleExist_ReturnsUserRole()
    {
        string expected = "Admin";
        _userRepositoryMock.Setup(x => x.GetUserRoles("1"))
            .ReturnsAsync(new List<string>
            {
                expected
            });

        var roles = await _sut.GetUserRoles("1");
        Assert.Equal(expected, roles.ToList()[0]);
    }

    [Fact]
    public async Task GetUserRoles_UserRoleDoesNotExist_ThrowsException()
    {
        _userRepositoryMock.Setup(x => x.GetUserRoles("1"))
            .ThrowsAsync(new Exception("No user roles found!"));

        await Assert.ThrowsAsync<Exception>(() => _sut.GetUserRoles("1"));
    }

    [Fact]
    public async Task GetAllUsersAndRoles_UsersExist_ReturnsUserWithRole()
    {
        string expected = "Admin";
        _userRepositoryMock.Setup(x => x.GetAllUsersAndRoles())
            .ReturnsAsync(new List<ApplicationUserDTO>
            {
                new ApplicationUserDTO
                {
                    Id = "1",
                    Roles = new string[] { expected }
                }
            });

        var roles = await _sut.GetAllUsersAndRoles();
        Assert.Equal(expected, roles.ToList()[0].Roles[0]);
    }

    [Fact]
    public async Task GetAllUsersAndRoles_UsersDoesNotExist_ThrowsException()
    {
        _userRepositoryMock.Setup(x => x.GetAllUsersAndRoles())
            .ThrowsAsync(new Exception("No users with roles found!"));

        await Assert.ThrowsAsync<Exception>(() => _sut.GetAllUsersAndRoles());
    }

    [Fact]
    public async Task UpdateUser_UserNotFound_ThrowsException()
    {
        _userRepositoryMock.Setup(x => x.GetById("1"))
            .ThrowsAsync(new Exception("User does not exist!"));
        ApplicationUserDTO applicationUserDTO = new ApplicationUserDTO
        {
            Id = "1",
        };

        await Assert.ThrowsAsync<Exception>(() => _sut.UpdateUser(applicationUserDTO));
    }

    //[Fact]
    //public async Task UpdateUser_MailSet_UpdatesMail()
    //{
    //    _userRepositoryMock.Setup(x => x.UpdateEmail(It.IsAny<ApplicationUserDTO>()))
    //        .ReturnsAsync(null);
    //    ApplicationUserDTO applicationUserDTO = new ApplicationUserDTO
    //    {
    //        Id = "1",
    //    };

    //    await Assert.ThrowsAsync<Exception>(() => _sut.UpdateUser(applicationUserDTO));
    //}

    [Fact]
    public async Task DeleteUser_UserNotFound_ThrowsException()
    {
        string userId = "1";
        _userRepositoryMock.Setup(x => x.GetById(userId))
            .ThrowsAsync(new Exception("User does not exist!"));
        ApplicationUserDTO applicationUserDTO = new ApplicationUserDTO
        {
            Id = userId,
        };

        await Assert.ThrowsAsync<Exception>(() => _sut.DeleteUser(userId));
    }


}
