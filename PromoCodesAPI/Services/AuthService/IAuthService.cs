using System;
using System.Threading.Tasks;
using PromoCodesAPI.DTOs;

namespace PromoCodesAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginInUser(LoginDto loginDto);
        Task LogOut();
    }
}
