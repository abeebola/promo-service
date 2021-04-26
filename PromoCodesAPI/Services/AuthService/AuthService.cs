using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PromoCodesAPI.Data;
using PromoCodesAPI.DTOs;
using PromoCodesAPI.Helpers;
using PromoCodesAPI.Models;
using PromoCodesAPI.Services.UserService;

namespace PromoCodesAPI.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private ApplicationContext _context;
        private IUserService _userService;
        private readonly AppSettings _appSettings;

        public AuthService(ApplicationContext applicationContext, IUserService userService, IOptions<AppSettings> appSettings)
        {
            _context = applicationContext;
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        public async Task<LoginResponse> LoginInUser(LoginDto loginDto)
        {
            var user = await _userService.UserExistsByEmail(loginDto.Email);
            if (user == null || Authenticate(user, loginDto.Password) == null)
            {
                return null;
            }

            var token = GenerateToken(user);

            return new LoginResponse
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id.ToString(),
                Token = token
            };
        }

        public Task LogOut()
        {
            throw new NotImplementedException();
        }

        private User Authenticate(User user, string plainPassword)
        {
            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, plainPassword);
            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return user;
        }

        private string GenerateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.TokenSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }
                ),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
