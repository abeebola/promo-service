using System;
using System.Threading.Tasks;
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
            var newService = new Models.Service
            {
                Name = serviceDTO.Name,
                Description = serviceDTO.Description,
            };
            var ns = await _context.Services.AddAsync(newService);

            await _context.SaveChangesAsync();
            return newService.ToDTO();
        }

        public async Task<ServiceResponse> UpdateService(UpdateServiceDto serviceDTO)
        {
            throw new NotImplementedException();
        }
    }
}
