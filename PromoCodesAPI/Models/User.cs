using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PromoCodesAPI.DTOs;

namespace PromoCodesAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<Bonus> ServiceBonuses {get;set;}

        public UserResponse ToDTO()
        {
            return new UserResponse
            {
                Id = Id.ToString(),
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                ServiceBonuses = this.ServiceBonuses
            };
        }
    }
}
