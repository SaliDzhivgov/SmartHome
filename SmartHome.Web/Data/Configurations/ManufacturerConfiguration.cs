using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHome.Web.Data.Entities;

namespace SmartHome.Web.Data.Configurations
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasIndex(m => m.Name).IsUnique();

            builder.HasData(
                new Manufacturer { Id = 1, Name = "Philips Hue", Country = "Нидерландия", Website = "https://www.philips-hue.com" },
                new Manufacturer { Id = 2, Name = "Xiaomi", Country = "Китай", Website = "https://www.mi.com" },
                new Manufacturer { Id = 3, Name = "Shelly", Country = "България", Website = "https://www.shelly.com" },
                new Manufacturer { Id = 4, Name = "Google Nest", Country = "САЩ", Website = "https://store.google.com" },
                new Manufacturer { Id = 5, Name = "Samsung", Country = "Южна Корея", Website = "https://www.samsung.com" },
                new Manufacturer { Id = 6, Name = "TP-Link", Country = "Китай", Website = "https://www.tp-link.com" });
        }
    }
}