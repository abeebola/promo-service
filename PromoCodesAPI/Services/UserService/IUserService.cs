using System;
using static PromoCodesAPI.DTOs.UserDTO;
using System.Threading.Tasks;

namespace PromoCodesAPI.Services.UserService
{
    public interface IUserService
    {
        Task<UserResponse> AddUser(AddUserDto userDto);
    }
}
