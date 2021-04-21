using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromoCodesAPI.Controllers;
using PromoCodesAPI.DTOs;
using PromoCodesAPI.Services.ServiceService;
using Tests.Data;

namespace Tests
{
    [TestClass]
    public class TestServiceController
    {
        private readonly IServiceService _service;

        public TestServiceController()
        { 
            _service = new ServiceService(TestAppContext.GetContext());
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
        public async Task PostService_ShouldFail_WhenSameName()
        {
            var controller = new ServiceController(_service);
            var serviceDto = GetServiceDto();
            var result = await controller.AddService(serviceDto);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConflictObjectResult));
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
