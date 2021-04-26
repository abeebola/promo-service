using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PromoCodesAPI.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id}", Name = "GetById")]
        public async Task<IActionResult> GetById(string Id)
        {
            return Ok();
        }
    }
}
