using AutoFixture;
using Moq;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Repositories;
using StefansSuperShop.Services;
using Xunit;

namespace StefansSuperShop.Unit.Test.Services
{
    public class NewsletterTests
    {
        private Mock<INewsletterRepository> _newsletterRepoMock;
        private Mock<IUserRepository> _userRepoMock;

        private NewsletterService _sut;
        private Fixture _fixture = new Fixture();

        public NewsletterTests()
        {
            _newsletterRepoMock = new Mock<INewsletterRepository>();
            _userRepoMock = new Mock<IUserRepository>();
            _sut = new NewsletterService(_newsletterRepoMock.Object, _userRepoMock.Object);
        }

        [Fact]
        public async void CreateNewsletter_ValidInPut_ThrowsNoException()
        {
            Newsletter newsletter = _fixture.Create<Newsletter>();

            await _sut.CreateNewsletter(newsletter.Title, newsletter.Content);
        }

        [Fact]
        public async void CreateSentNewsletter_NewsletterIdDoesntExist_ThrowsException()
        {
            int newsletterId = 1;

            _newsletterRepoMock.Setup(x => x.GetById(1))
                .ReturnsAsync((Newsletter) null);

            await Assert.ThrowsAsync<Exception>(() => _sut.CreateSentNewsletter(newsletterId));
        }

        [Fact]
        public async void CreateSentNewsletter_NewsletterIdExist_ThrowsNoException()
        {
            int newsletterId = 1;

            Newsletter newsletter = _fixture.Create<Newsletter>();

            _newsletterRepoMock.Setup(x => x.GetById(1))
                .ReturnsAsync(newsletter);

            var users = _fixture.Create<IEnumerable<ApplicationUser>>();

            _userRepoMock.Setup(x => x.GetAll())
                .ReturnsAsync(users);


            await Assert.ThrowsAsync<Exception>(() => _sut.CreateSentNewsletter(newsletterId));
        }

    }
}
