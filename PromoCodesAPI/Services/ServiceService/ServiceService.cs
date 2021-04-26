using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PromoCodesAPI.Data;
using PromoCodesAPI.DTOs;
using PromoCodesAPI.Models;

namespace PromoCodesAPI.Services.ServiceService
{
    public class ServiceService : IServiceService
    {
        private ApplicationContext _context;

        public ServiceService(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task<List<ServiceResponse>> GetAll(string name, DateTime? lastTimestamp)
        {
            var query = _context.Services.AsQueryable();

            if (name != null)
            {
                query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            if (lastTimestamp != null)
            {
                query = query.Where(x => x.CreatedAt < lastTimestamp);
            }
            return await query.OrderByDescending(x => x.CreatedAt)
                .Select(x => x.ToDTO())   
                .ToListAsync();
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

        public async Task<ServiceResponse> GetById(string id)
        {
            var service = await _context.Services.Include(x => x.UserBonuses).ThenInclude(y => y.User)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            return service?.ToDTO();
        }

        public async Task<ServiceResponse> AddBonus(AddBonusDto bonusDto, string userId)
        {
            var service = await _context.Services.FirstOrDefaultAsync(x => x.Id == bonusDto.ServiceId);
            if (service == null)
            {
                return null;
            }

            var user = await _context.Users.Include(x => x.ServiceBonuses).FirstOrDefaultAsync(x => x.Id == Guid.Parse(userId));
            if (user == null)
            {
                return null;
            }

            // Bonus already exists for this user
            if (user.ServiceBonuses.Any(x => x.ServiceId == bonusDto.ServiceId))
            {
                throw new Exception("Conflict");
            }

            var bonus = new Bonus
            {
                ServiceId = service.Id,
                UserId = user.Id
            };

            await _context.ServiceBonuses.AddAsync(bonus);
            await _context.SaveChangesAsync();
            var result = await _context.Services.Include(x => x.UserBonuses).ThenInclude(y => y.User).FirstOrDefaultAsync(x => x.Id == service.Id);
            return result?.ToDTO();
        }
    }
}
