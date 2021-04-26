using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PromoCodesAPI.Models;

namespace PromoCodesAPI.DTOs
{
    public class AddServiceDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class AddBonusDto
    {
        public string PromoCode { get; set; }
        [Required]
        public Guid ServiceId { get; set; }
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

        public ICollection<Bonus> Bonuses {get;set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
