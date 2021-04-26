using System;
using System.Threading.Tasks;
using static PromoCodesAPI.DTOs.UserDTO;

namespace PromoCodesAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginInUser(LoginDto loginDto);
        Task LogOut();
    }
}
