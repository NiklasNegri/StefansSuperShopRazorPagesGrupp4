using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using StefansSuperShop.Data.Entities;
using StefansSuperShop.Data.Helpers;
using StefansSuperShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefansSuperShop.Unit.Test.Respositories;

public class UserRepositoryTests :IDisposable
{
    //private Mock<IMapper> _mapperMock;
    //private ApplicationDbContext _context;
    //private Mock<UserManager<ApplicationUser>> _userManagerMock;

    //private UserRepository _sut;
    //public UserRepositoryTests()
    //{
    //    _mapperMock = new Mock<IMapper>();

    //    var connection = new SqliteConnection("Filename=:memory:");
    //    connection.Open();
    //    var contextOption = new DbContextOptionsBuilder<ApplicationDbContext>()
    //        .UseSqlite(connection)
    //        .Options;
    //    _context = new ApplicationDbContext(contextOption);
    //    _context.Database.EnsureCreated();

    //    _userManagerMock = new Mock<UserManager<ApplicationUser>>();

    //    _sut = new UserRepository(_userManagerMock.Object, _context, _mapperMock.Object);
    //}

    public void Dispose()
    {
        //_context.Dispose();
    }

    //[Fact]
    //public void RegisterUser()
    //{

    //}
}
