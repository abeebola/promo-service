using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoCodesAPI.DTOs;
using PromoCodesAPI.Services.ServiceService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PromoCodesAPI.Controllers
{
    [ApiController]
    [Route("/services")]
    [Authorize]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        /// <summary>
        /// Lists all services.
        /// </summary>
        /// <remarks>
        /// The endpoint supports filtering by name and also last timestamp.
        /// The last timestamp value is used for infinite scrolling
        /// </remarks>
        /// <param name="name">
        /// The name to filter by
        /// </param>
        /// <param name="lastTimestamp">The timestamp of the last fetched service.</param>
        /// <returns>A list of all Services</returns>
        /// <response code="200"></response>
        /// <response code="401">If the current request isn't from an authenticated user</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<List<ServiceResponse>> GetAll(
            string name = null, [FromQuery(Name = "last_ts")]DateTime? lastTimestamp = null
            )
        {
            return await _serviceService.GetAll(name, lastTimestamp);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ServiceResponse> GetByID(string id)
        {
            return await _serviceService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddService(AddServiceDto serviceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var service = await _serviceService.AddService(serviceDto);
                return CreatedAtAction(nameof(GetByID), new { service.Id }, service);
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower() == "conflict")
                {
                    return Conflict($"Service with the name: {serviceDto.Name} already exists.");
                }

                return StatusCode(500);
            }
        }
    }
}
