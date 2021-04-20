using System;
using System.ComponentModel.DataAnnotations;

namespace PromoCodesAPI.DTOs
{
    public class AddServiceDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class UpdateServiceDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ServiceResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
