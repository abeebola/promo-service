using System;
using System.ComponentModel.DataAnnotations;
using PromoCodesAPI.DTOs;

namespace PromoCodesAPI.Models
{
    public class Service
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ServiceResponse ToDTO()
        {
            return new ServiceResponse
            {
                CreatedAt = this.CreatedAt,
                Name = this.Name,
                Description = this.Description,
                Id = this.Id.ToString(),
                UpdatedAt = this.UpdatedAt
            };
        }
    }
}
