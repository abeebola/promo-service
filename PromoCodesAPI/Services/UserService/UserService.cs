using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PromoCodesAPI.Data;
using PromoCodesAPI.DTOs;
using PromoCodesAPI.Models;

namespace PromoCodesAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private ApplicationContext _context;

        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<UserDTO.UserResponse> AddUser(UserDTO.AddUserDto userDto)
        {
            // Check if name already exists
            var exists = await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower() == userDto.Email.ToLower());

            if (exists != null)
            {
                throw new Exception("Conflict");
            }

            var newUser = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Password = userDto.Password
            };

            newUser.Password = GeneratePasswordHash(newUser, newUser.Password);
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return newUser.ToDTO();
        }

        private string GeneratePasswordHash(User user, string password)
        {
            var hasher = new PasswordHasher<User>();
            return hasher.HashPassword(user, password);
        }
    }
}
