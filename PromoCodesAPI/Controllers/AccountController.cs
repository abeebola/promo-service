using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromoCodesAPI.Services.UserService;
using static PromoCodesAPI.DTOs.UserDTO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PromoCodesAPI.Controllers
{
    [ApiController]
    [Route("/account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(AddUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var newUser = await _userService.AddUser(userDto);
                return CreatedAtRoute(nameof(UserController.GetById), new { newUser.Id }, newUser);

            } catch (Exception ex)
            {
                if (ex.Message.ToLower() == "conflict")
                {
                    return Conflict($"User with the email: {userDto.Email} already exists.");
                }
            }

            return StatusCode(500);
            
        }
    }
}
