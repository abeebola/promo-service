using System;
using System.Text.Json.Serialization;

namespace PromoCodesAPI.Models
{
    public class Bonus
    {
        public Bonus() {}
        public Guid ServiceId { get; set; }
        [JsonIgnore]
        public Service Service { get; set; }

        public Guid UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
