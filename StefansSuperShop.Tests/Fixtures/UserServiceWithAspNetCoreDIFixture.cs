using Microsoft.Extensions.DependencyInjection;
using StefansSuperShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefansSuperShop.Test.Fixtures;

public class UserServiceWithAspNetCoreDIFixture : IDisposable
{
    private ServiceProvider _serviceProvider;

    public UserServiceWithAspNetCoreDIFixture()
    {
        var service = new ServiceCollection();
        //service.AddScoped<IUserService, UserServiceWithAspNetCoreDIFixture>();

        _serviceProvider = service.BuildServiceProvider();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
