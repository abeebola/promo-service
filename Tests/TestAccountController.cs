using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromoCodesAPI.Controllers;
using PromoCodesAPI.Data;
using PromoCodesAPI.Services.ServiceService;
using PromoCodesAPI.Services.UserService;
using Tests.Data;
using static PromoCodesAPI.DTOs.UserDTO;

namespace Tests
{
    [TestClass]
    public class TestAccountController
    {
        private readonly IUserService _service;
        private readonly ApplicationContext _applicationContext;

        public TestAccountController()
        {
            _applicationContext = TestAppContext.GetContext();
            _service = new UserService(_applicationContext);
        }

        [TestMethod]
        public async Task RegisterUser_ShouldReturnCreatedStatus()
        {
            var controller = new AccountController(_service);
            var userDto = GetUserDto();
            var result = await controller.Register(userDto) as CreatedAtRouteResult;
            var user = result.Value as UserResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(user.Id, "".GetType());
            Assert.AreEqual(result.StatusCode, 201);
        }

        [TestMethod]
        public async Task PostUser_ShouldFail_WhenSameEmail()
        {
            var controller = new AccountController(_service);
            var userDto = GetUserDto();
            var result = await controller.Register(userDto);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConflictObjectResult));
        }

        [ClassCleanup]
        public async static Task RemoveUsers()
        {
            var context = TestAppContext.GetContext();
            var allUsers = context.Users;
            context.Users.RemoveRange(allUsers);
            await context.SaveChangesAsync();
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
