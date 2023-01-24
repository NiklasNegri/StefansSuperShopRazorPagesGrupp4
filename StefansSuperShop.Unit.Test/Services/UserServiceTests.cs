using AutoFixture;
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
}
