using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PromoCodesAPI.Controllers;
using PromoCodesAPI.Data;
using PromoCodesAPI.DTOs;
using PromoCodesAPI.Services.ServiceService;
using PromoCodesAPI.Services.UserService;
using Tests.Data;

namespace Tests
{
    [TestClass]
    public class TestServiceController
    {
        private readonly IServiceService _service;
        public readonly ApplicationContext _context;
        private readonly ServiceController _controller;

        public TestServiceController()
        { 
            _context = TestAppContext.GetContext();
            _service = new ServiceService(_context);
            _controller = new ServiceController(_service);
        }

        [TestMethod]
        public async Task PostService_ShouldReturnvalidService()
        {
            var controller = new ServiceController(_service);
            var serviceDto = GetServiceDto();
            var result = await controller.AddService(serviceDto) as CreatedAtActionResult;
            var service = result.Value as ServiceResponse;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(service.Id, "".GetType());
            Assert.AreEqual(result.StatusCode, 201);
        }

        [TestMethod]
        public async Task GetAll_SHouldReturnAllServices()
        {
            var controller = new ServiceController(_service);
            var services = await controller.GetAll();

            Assert.IsNotNull(services);
            Assert.IsTrue(services.Count > 0);
        }

        [TestMethod]
        public async Task PostService_ShouldReturnServiceWithName()
        {
            var controller = new ServiceController(_service);
            // Add a new service with a different name
            var serviceDto = GetServiceDto();
            serviceDto.Name = "Another Service";
            await controller.AddService(serviceDto);

            // get all services with matching names
            var partialName = "Sample";
            var services = await controller.GetAll(partialName);

            Assert.IsNotNull(services);
            Assert.IsTrue(services.Count == 1);
            Assert.AreEqual("Sample service", services[0].Name);
        }

        [TestMethod]
        public async Task PostService_ShouldFail_WhenSameName()
        {
            var controller = new ServiceController(_service);
            var serviceDto = GetServiceDto();
            var result = await controller.AddService(serviceDto);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConflictObjectResult));
        }

        [TestMethod]
        public async Task PostService_ShouldReturnServiceWithId()
        {
            var controller = new ServiceController(_service);

            // get all services
            var services = await controller.GetAll();
            var service = services[0];

            // fetch service by ID
            var returnedService = await controller.GetByID(service.Id);

            Assert.IsNotNull(returnedService);
            Assert.AreEqual(returnedService.Id, service.Id);
        }

        [TestMethod]
        public async Task AddBonus_ShouldReturnServiceAndBonus()
        {
            var controller = new ServiceController(_service);
            var serviceDto = GetServiceDto();
            var service = (await controller.GetAll(serviceDto.Name))[0];
            var user = await _context.Users.FirstOrDefaultAsync();

            var bonusDto = new AddBonusDto 
            {
                PromoCode = "itpromocodes",
                ServiceId = Guid.Parse(service.Id),
            };

            // mock current user user
            var claims = new ClaimsIdentity(
                 new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }
            );

            var claimsPrincipal = new ClaimsPrincipal(claims);
            var httpContext = new DefaultHttpContext();
            httpContext.User = claimsPrincipal;

            var controllerContext = new ControllerContext(){
                HttpContext = httpContext
            };

            controller.ControllerContext = controllerContext;

            // Add bonus
            var result = await controller.AddBonus(bonusDto) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(ServiceResponse));
        }

        AddServiceDto GetServiceDto()
        {
            return new AddServiceDto
            {
                Name = "Sample service",
                Description = "Sample Description"
            };
        }
    }
}
