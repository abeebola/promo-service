﻿using System;
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
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<List<ServiceResponse>> GetAll(string name = null)
        {
            return await _serviceService.GetAll(name);
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
