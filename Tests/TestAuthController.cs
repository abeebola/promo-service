using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromoCodesAPI.Controllers;
using PromoCodesAPI.Data;
using PromoCodesAPI.Services.AuthService;
using PromoCodesAPI.Services.UserService;
using Tests.Data;
using Tests.Helpers;
using PromoCodesAPI.DTOs;

namespace Tests
{
    [TestClass]
    public class TestAuthController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ApplicationContext _context;
        private readonly AuthController _authController;
        private readonly AccountController _accountController;

        public TestAuthController()
        {
            var appSettings = AppSettings.GetAppSettings();
            _context = TestAppContext.GetContext();
            _userService = new UserService(_context);
            _authService = new AuthService(TestAppContext.GetContext(), _userService, appSettings);
            _authController = new AuthController(_authService);
            _accountController = new AccountController(_userService);
        }

        [TestMethod]
        public async Task LoginUser_ShouldReturnUserAndToken()
        {
            // Clear previous users if any exists
            await RemoveUsers();

            // Setup
            var userDto = GetUserDto();

            // Register new user
            var result = await _accountController.Register(userDto) as CreatedAtRouteResult;
            var user = result.Value as UserResponse;

            // Log user in
            var loginResult = await _authController.Login(new LoginDto
            {
                Email = user.Email,
                Password = userDto.Password
            }) as OkObjectResult;

            var loggedInUser = loginResult.Value as LoginResponse;

            Assert.AreEqual(loginResult.StatusCode, 200);
            Assert.IsNotNull(loginResult);
            Assert.IsNotNull(loggedInUser);
            Assert.IsTrue(loggedInUser.Token.Length > 0);          
        }

        [TestMethod]
        public async Task LoginUser_ShouldFail_IfUserNotFound()
        {
            // Setup nonexistent user
            var user = new LoginDto
            {
                Email = "david@cia.gov.us",
                Password = "boring_job"
            };

            // Log user in
            var loginResult = await _authController.Login(user);

            Assert.IsNotNull(loginResult);
            Assert.IsInstanceOfType(loginResult, typeof(BadRequestObjectResult));
        }

        private async Task RemoveUsers()
        {
            var allUsers = _context.Users;
            _context.Users.RemoveRange(allUsers);
            await _context.SaveChangesAsync();
        }

        private AddUserDto GetUserDto()
        {
            return new AddUserDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@doe.com",
                Password = "pass123",
            };
        }
    }
}
