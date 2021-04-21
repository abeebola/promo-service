using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PromoCodesAPI.Data;
using PromoCodesAPI.Models;

namespace Tests.Data
{

    public class TestAppContext
    {
        public static ApplicationContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: "promo_db")
            .Options;

            return new ApplicationContext(options);
        }
        //public DbSet<Service> Services { get; set; }
        //public DbSet<User> Users { get; set; }

        //public async Task SaveChangesAsync()
        //{
        //    await Task.Run(() => 0);
        //}

        //public void MarkAsModified(Service item) { }
        //public void Dispose() { }
    }
}
