using Microsoft.EntityFrameworkCore;
using SmartHome.Web.Data.Entities;

namespace SmartHome.Web.Data
{
    public class SmartHomeDbContext : DbContext
    {
        public SmartHomeDbContext(DbContextOptions<SmartHomeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Manufacturer> Manufacturers { get; set; } = null!;

        public DbSet<Device> Devices { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Зарежда всички класове IEntityTypeConfiguration от папка Data/Configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartHomeDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}