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
    }
}
