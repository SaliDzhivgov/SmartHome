using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHome.Web.Data.Entities;

namespace SmartHome.Web.Data.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasIndex(r => r.Name).IsUnique();

            builder.HasData(
                new Room { Id = 1, Name = "Хол", Floor = 1, Description = "Основното помещение за почивка и гости." },
                new Room { Id = 2, Name = "Кухня", Floor = 1, Description = "Кухня с трапезария." },
                new Room { Id = 3, Name = "Спалня", Floor = 2, Description = "Голямата спалня." },
                new Room { Id = 4, Name = "Детска стая", Floor = 2, Description = null },
                new Room { Id = 5, Name = "Баня", Floor = 2, Description = "Баня с тоалетна." },
                new Room { Id = 6, Name = "Гараж", Floor = 0, Description = "Гараж и вход към двора." });
        }
    }
}