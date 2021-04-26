using System;
using static PromoCodesAPI.DTOs.UserDTO;
using System.Threading.Tasks;
using PromoCodesAPI.Models;

namespace PromoCodesAPI.Services.UserService
{
    public interface IUserService
    {
        Task<UserResponse> AddUser(AddUserDto userDto);
        Task<User> GetById(string id);
        Task<User> UserExistsByEmail(string email);
    }
}
