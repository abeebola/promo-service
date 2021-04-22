using System;
using System.ComponentModel.DataAnnotations;
using static PromoCodesAPI.DTOs.UserDTO;

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

        public UserResponse ToDTO()
        {
            return new UserResponse
            {
                Id = Id.ToString(),
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt
            };
        }
    }
}
