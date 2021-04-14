using Microsoft.EntityFrameworkCore;
using ProvinciasApi.Entities;

namespace ProvinciasApi.Data
{
    public class ProvinceContext : DbContext
    {
        public ProvinceContext(DbContextOptions<ProvinceContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = new User() { Id = 1, Username = "test", Password = "123456", Email = "test@test.com", Fullname ="test mock"};

            modelBuilder.Entity<User>().HasData(new User[] { user });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> User { get; set; }
        public DbSet<LogEntry> LogEntry { get; set; }
    }
}
