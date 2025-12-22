using CoffeeBreakAPI.Dtos;
using CoffeeBreakAPI.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBreakAPI.Tests.UnitTest
{
    [TestClass]
    public class AuthControllerTests
    {
        private Mock<IAuthService>? _authServiceMock;
        private AuthController? _controller;

        [TestInitialize]
        public void Setup()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AuthController(_authServiceMock.Object);
        }

        [TestMethod]
        public async Task Register_ReturnsOk_WhenUserCreated()
        {
            // Arrange
            var dto = new RegisterDto
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                Password = "Password123!"
            };

            _authServiceMock
                .Setup(s => s.RegisterAsync(dto))
                .ReturnsAsync(new Models.User { Email = dto.Email, FirstName = dto.FirstName });

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("User registered successfully.", okResult.Value);
        }

        [TestMethod]
        public async Task Register_ReturnsBadRequest_WhenUserExists()
        {
            // Arrange
            var dto = new RegisterDto { Email = "test@example.com" };
            _authServiceMock.Setup(s => s.RegisterAsync(dto)).ReturnsAsync((Models.User?)null);

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var badRequest = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreEqual(400, badRequest.StatusCode);
            Assert.AreEqual("Invalid data or user already exists", badRequest.Value);
        }

        [TestMethod]
        public async Task Login_ReturnsOk_WhenCredentialsAreValid()
        {
            // Arrange
            var dto = new LoginDto { Email = "test@example.com", Password = "Password123!" };
            _authServiceMock.Setup(s => s.LoginAsync(dto)).ReturnsAsync("FakeJWTToken");

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("FakeJWTToken", okResult.Value);
        }

        [TestMethod]
        public async Task Login_ReturnsBadRequest_WhenCredentialsInvalid()
        {
            // Arrange
            var dto = new LoginDto { Email = "test@example.com", Password = "WrongPassword" };
            _authServiceMock.Setup(s => s.LoginAsync(dto)).ReturnsAsync((string?)null);

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var badRequest = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreEqual(400, badRequest.StatusCode);
            Assert.AreEqual("Invalid email or password.", badRequest.Value);
        }
    }
}
