using System;
using Microsoft.EntityFrameworkCore;
using PromoCodesAPI.Models;

namespace PromoCodesAPI.Data
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
