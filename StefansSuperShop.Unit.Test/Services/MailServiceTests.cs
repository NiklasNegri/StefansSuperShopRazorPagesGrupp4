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
            MailSettings options = new MailSettings()
            {
                DisplayName = "Display Name",
                From = "test@test.se",
                Host = "host.host",
                Port = 000,
                UserName = "test@username.se",
                Password = "Abc123!",
                UseSSL = false,
                UseStartTls = true
            };

            _settingsMock = new Mock<IOptions<MailSettings>>();
            _settingsMock.Setup(x => x.Value).Returns(options);

            _sut = new MailService(_settingsMock.Object);
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
