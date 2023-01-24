using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StefansSuperShop.Services;
using StefansSuperShop.Data.Model;
using Microsoft.Extensions.Options;
using StefansSuperShop.Configuration;

namespace StefansSuperShop.Unit.Test.Services
{
    public class MailServiceTests
    {
        private readonly Mock<IOptions<MailSettings>> _settingsMock;
        private readonly MailService _sut;

        public MailServiceTests()
        {
            _settingsMock = new Mock<IOptions<MailSettings>>();
            _sut = new MailService(_settingsMock.Object);
        }

        [Fact]
        public async void SendAsync_ValidInput_SendMail()
        {
            
        }

        [Fact]
        public async void SendAsync_InvalidInput_ShowError()
        {
            var mailData = new MailData()
            {

            };
        }
    }
}
