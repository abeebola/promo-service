using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PromoCodesAPI.Data;
using PromoCodesAPI.DTOs;

namespace PromoCodesAPI.Services.ServiceService
{
    public class ServiceService : IServiceService
    {
        private ApplicationContext _context;
        public ServiceService(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task<ServiceResponse> AddService(AddServiceDto serviceDTO)
        {
            // Check if name already exists
            var exists = await _context.Services
                .FirstOrDefaultAsync(x => x.Name.ToLower() == serviceDTO.Name.ToLower());

            if (exists != null)
            {
                throw new Exception("Conflict");
            }

            var newService = new Models.Service
            {
                Name = serviceDTO.Name,
                Description = serviceDTO.Description,
            };
            await _context.Services.AddAsync(newService);

            await _context.SaveChangesAsync();
            return newService.ToDTO();
        }

        public async Task<ServiceResponse> UpdateService(UpdateServiceDto serviceDTO)
        {
            throw new NotImplementedException();
        }
    }
}
