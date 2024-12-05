using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Repositories;
using UseCases;
using Entities;

namespace Test
{
    public class LoginTest
    {
        [Fact]
        public async Task LoginAsync_ShouldReturnUserNotFound()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetAllAsync())
                                .ReturnsAsync(new List<User>());

            var authenticationManager = new AuthenticationManager(mockUserRepository.Object);

            var result = await authenticationManager.LoginAsync("nonexistentuser", "password");

            // Assert
            Assert.Equal(LoginResultCodes.UserNotFound, result.ResultCode);
            Assert.Equal("User is not found!", result.Message);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnWrongPassword()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetAllAsync())
                              .ReturnsAsync(new List<User>
                              {
                              new User { Username = "testuser", Password = new PasswordHasher<string>().HashPassword("testuser", "correctpassword"), Email = "", Phone = "" }
                              });

            var authenticationManager = new AuthenticationManager(mockUserRepository.Object);

            var result = await authenticationManager.LoginAsync("testuser", "wrongpassword");

            Assert.Equal(LoginResultCodes.WrongPassword, result.ResultCode);
            Assert.Equal("Wrong password!", result.Message);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnSuccess()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetAllAsync())
                              .ReturnsAsync(new List<User>
                              {
                              new User { Username = "testuser", Password = new PasswordHasher<string>().HashPassword("testuser", "correctpassword"), Email = "", Phone = "" }
                              });

            var authenticationManager = new AuthenticationManager(mockUserRepository.Object);

            var result = await authenticationManager.LoginAsync("testuser", "correctpassword");

            Assert.Equal(LoginResultCodes.Success, result.ResultCode);
            Assert.Equal("Success", result.Message);
        }
    }
}
