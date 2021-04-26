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

        public async Task<UserResponse> AddUser(AddUserDto userDto)
        {
            // Check if name already exists
            var exists = await UserExistsByEmail(userDto.Email);

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

        public async Task<User> GetById(string id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
        }

        public async Task<User> UserExistsByEmail(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }

        private string GeneratePasswordHash(User user, string password)
        {
            var hasher = new PasswordHasher<User>();
            return hasher.HashPassword(user, password);
        }
    }
}
