using AutoFixture;
using Microsoft.Extensions.Options;
using Moq;
using StefansSuperShop.Configuration;
using StefansSuperShop.Data.Model;
using StefansSuperShop.Services;

namespace StefansSuperShop.Unit.Test.Services
{
    public class MailServiceTests
    {
        private readonly Mock<IOptions<MailSettings>> _settingsMock;
        private Fixture _fixture = new ();
        private readonly MailService _sut;

        public MailServiceTests()
        {
            _settingsMock = new Mock<IOptions<MailSettings>>();
            _sut = new MailService(_settingsMock.Object);

            _settingsMock.Setup(x => x.Value.DisplayName).Returns("Display Name");
            _settingsMock.Setup(x => x.Value.From).Returns("test@test.se");
            _settingsMock.Setup(x => x.Value.Host).Returns("host.host");
            _settingsMock.Setup(x => x.Value.Port).Returns(000);
            _settingsMock.Setup(x => x.Value.UserName).Returns("test@username.se");
            _settingsMock.Setup(x => x.Value.Password).Returns("Abc123!");
            _settingsMock.Setup(x => x.Value.UseSSL).Returns(false);
            _settingsMock.Setup(x => x.Value.UseStartTls).Returns(true);
        }

        [Fact]
        public async void SendAsync_ValidInput_ThrowsNoErrors()
        {
            MailData mailData = _fixture.Create<MailData>();

            await _sut.SendAsync(mailData, new CancellationToken());
        }

        [Fact]
        public async void SendAsync_NoSubject_ThrowsErrors()
        {
            MailData mailData = _fixture.Create<MailData>();

            mailData.Subject = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.SendAsync(mailData, new CancellationToken()));
        }

        [Fact]
        public async void SendAsync_NoRecipients_ThrowsErrors()
        {
            MailData mailData = _fixture.Create<MailData>();

            mailData.To = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.SendAsync(mailData, new CancellationToken()));
        }
    }
}
