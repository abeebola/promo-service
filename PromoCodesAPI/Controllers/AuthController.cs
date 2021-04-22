﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PromoCodesAPI.Controllers
{
    [Route("/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
