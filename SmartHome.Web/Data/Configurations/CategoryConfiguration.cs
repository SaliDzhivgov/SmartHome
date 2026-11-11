using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHome.Web.Data.Entities;

namespace SmartHome.Web.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasIndex(c => c.Name).IsUnique();

            builder.HasData(
                new Category { Id = 1, Name = "Осветление", Icon = "bi-lightbulb", Description = "Лампи, LED ленти и ключове." },
                new Category { Id = 2, Name = "Климат", Icon = "bi-thermometer-half", Description = "Термостати, климатици и сензори за температура." },
                new Category { Id = 3, Name = "Сигурност", Icon = "bi-shield-lock", Description = "Камери, брави и датчици за движение." },
                new Category { Id = 4, Name = "Контакти", Icon = "bi-plug", Description = "Умни контакти и релета." },
                new Category { Id = 5, Name = "Мултимедия", Icon = "bi-tv", Description = "Телевизори и тонколони." },
                new Category { Id = 6, Name = "Битова техника", Icon = "bi-gear-wide-connected", Description = "Хладилници, перални и други уреди." });
        }
    }
}