using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using StefansSuperShop.Configuration;
using StefansSuperShop.Data.DTOs;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Data.Helpers;
using StefansSuperShop.Data.Model;
using StefansSuperShop.Pages.BackendTests.Mail;
using StefansSuperShop.Repositories;
using StefansSuperShop.Services;

namespace StefansSuperShop.Test;

// we use moq to mock the dependencies, not the thing we want to test
public class MoqTests
{
    UserService _userService;
    public MoqTests()
    {
        var userRepositoy =
            new Mock<IUserRepository>();

        // when setting up a method you set the method parameters to the type of filter that is looked for
        // to give different results
        userRepositoy
            .Setup(m => m.GetById(It.IsAny<string>()))
            .ReturnsAsync(new ApplicationUser()
            {
                Email = "test@test.com",
                UserName = "TestUserName",
            });
        _userService = new UserService(userRepositoy.Object);

        var mapperMock = new Mock<IMapper>();
        mapperMock
            .Setup(m => m.Map<ApplicationUserDTO, ApplicationUser>(It.IsAny<ApplicationUserDTO>()))
            .Returns(new ApplicationUser());

        var mapperConfiguration = new MapperConfiguration(
            cfg => cfg.AddProfile<AutoMapperProfile>());
        var mapper = new Mapper(mapperConfiguration);
    }

    [Fact]
    public async Task Tester_Async()
    {
        // Arrange

        // Act
        var user = await _userService.GetById("1");

        // Assert
        Assert.Equal("TestUserName", user.UserName);
    }

    [Fact]
    public async Task TestMailService()
    {
        // Arrange
        var mailServiceMock = new Mock<IMailService>();

        mailServiceMock
            .Setup(m => m.SendAsync(It.IsAny<MailData>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        MailService mailService = new MailService(null);

        MailSenderModel mailSender = new MailSenderModel(mailServiceMock);

        // Act

        // Assert
    }
}
