using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PromoCodesAPI.Models;

namespace PromoCodesAPI.Data
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Bonus> ServiceBonuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Bonus>().HasKey(i => new {i.ServiceId, i.UserId});
        }
    }
}
