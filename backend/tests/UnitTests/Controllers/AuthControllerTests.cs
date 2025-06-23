using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PVDNOTE.Backend.Api.DTO;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.Infrastructure.Data;
using Xunit;
using Backend.Tests.UnitTests.Utils;

namespace Backend.Tests.UnitTests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<DBContext> mockContext;
        private readonly AuthController controller;

        public AuthControllerTests()
        {
            var options = new DbContextOptionsBuilder<DBContext>().Options;
            mockContext = new Mock<DBContext>(options);
            controller = new AuthController(mockContext.Object);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenLoginExists()
        {

            var existingUser = new User { Login = "test@test.com" };
            var mockUsers = new List<User> { existingUser };
            
            var mockUsersSet = DbSetMockHelper.CreateMockDbSet(mockUsers);
            mockContext.SetupGet(x => x.Users).Returns(mockUsersSet.Object);

            var dto = new UserRegisterDto { Login = "test@test.com", Password = "123" };
            

            var result = await controller.Register(dto);
            

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Email уже зарегистрирован", GetMessageValue(badRequest.Value)?.Trim());
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenPasswordIsInvalid()
        {
            var existingUser = new User 
            { 
                Login = "valid@test.com", 
                Password = BCrypt.Net.BCrypt.HashPassword("correct_password") 
            };

            var mockUsers = new List<User> { existingUser };
            var mockUsersSet = DbSetMockHelper.CreateMockDbSet(mockUsers);
            mockContext.SetupGet(x => x.Users).Returns(mockUsersSet.Object);

            var invalidLoginDto = new UserLoginDto 
            { 
                Login = "valid@test.com", 
                Password = "wrong_password" 
            };
            
            var result = await controller.Login(invalidLoginDto);
            
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("неверный пароль или email", GetMessageValue(unauthorizedResult.Value));
        }

        private static string? GetMessageValue(object? resultValue)
        {
            var property = resultValue?.GetType().GetProperty("message");
            return property?.GetValue(resultValue) as string;
        }
    }
}