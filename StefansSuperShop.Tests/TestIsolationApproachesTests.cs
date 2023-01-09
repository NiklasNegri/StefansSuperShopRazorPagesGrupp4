//using AutoMapper;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Data.Sqlite;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using StefansSuperShop.Data.Entities;
//using StefansSuperShop.Data.Helpers;
//using StefansSuperShop.Repositories;
//using StefansSuperShop.Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace StefansSuperShop.Test;

//public class TestIsolationApproachesTests
//{
//    [Fact]
//    public async void ATest()
//    {
//        // Arrange
//        var connection = new SqliteConnection("Data Source=:memory:");
//        connection.Open();

//        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
//            .UseSqlite(connection);

//        var dbContext = new ApplicationDbContext(optionsBuilder.Options);
//        dbContext.Database.Migrate();

//        var userRepository =
//            new UserRepository(
//                dbContext.GetService<UserManager<ApplicationUser>>(),
//                dbContext,
//                dbContext.GetService<IMapper>());

//        var userService = new UserService(userRepository);

//        // Act
//        var result = await userService.GetAll();
//    }
//}
