using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PromoCodesAPI.DTOs;
using PromoCodesAPI.Models;

namespace PromoCodesAPI.Services.ServiceService
{
    // I spent too long thinking of a
    // suitable name for this. Had to skip
    public interface IServiceService
    {
        Task<List<ServiceResponse>> GetAll();
        Task<ServiceResponse> AddService(AddServiceDto serviceDTO);
        Task<ServiceResponse> UpdateService(UpdateServiceDto serviceDTO);
    }
}
