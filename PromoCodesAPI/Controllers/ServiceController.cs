using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromoCodesAPI.DTOs;
using PromoCodesAPI.Services.ServiceService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PromoCodesAPI.Controllers
{
    [ApiController]
    [Route("/services")]
    public class PromoServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public PromoServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Okay.");
        }

        [HttpPost]
        public async Task<IActionResult> AddService(AddServiceDto serviceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var service = await _serviceService.AddService(serviceDto);
            return CreatedAtAction(nameof(Index), new { Id = service.Id }, service);
        }
    }
}
