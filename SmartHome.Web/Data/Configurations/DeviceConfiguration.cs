using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHome.Web.Data.Entities;

namespace SmartHome.Web.Data.Configurations
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasIndex(d => d.SerialNumber).IsUnique();

            // Restrict: не позволяваме изтриване на стая/категория/производител, ако има устройства
            builder.HasOne(d => d.Room)
                .WithMany(r => r.Devices)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Category)
                .WithMany(c => c.Devices)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Manufacturer)
                .WithMany(m => m.Devices)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                Create(1, "Лампа над дивана", "PH-HUE-1001", 9.5, new DateTime(2024, 3, 12), true, 1, 1, 1, "Сменя цвета според времето на деня."),
                Create(2, "Смарт телевизор", "SS-TV-55Q80", 120, new DateTime(2023, 11, 20), false, 1, 5, 5, null),
                Create(3, "Термостат", "GN-TH-3010", 3, new DateTime(2024, 1, 8), true, 1, 2, 4, "Поддържа 22 °C през деня."),
                Create(4, "Контакт за кафемашина", "SH-PLUG-2201", 1200, new DateTime(2024, 5, 2), false, 2, 4, 3, "Включва се автоматично в 7:00."),
                Create(5, "LED лента под шкафовете", "XI-LED-7781", 14, new DateTime(2024, 5, 2), true, 2, 1, 2, null),
                Create(6, "Хладилник с Wi-Fi", "SS-RF-9020", 150, new DateTime(2023, 9, 15), true, 2, 6, 5, null),
                Create(7, "Нощна лампа", "PH-HUE-1002", 6, new DateTime(2024, 2, 14), false, 3, 1, 1, null),
                Create(8, "Климатик", "XI-AC-1250", 950, new DateTime(2023, 6, 30), false, 3, 2, 2, "Профилактика веднъж годишно."),
                Create(9, "Бебефон с камера", "TP-CAM-C200", 4.5, new DateTime(2024, 4, 1), true, 4, 3, 6, null),
                Create(10, "Нагревател за кърпи", "SH-RELAY-1PM", 400, new DateTime(2023, 12, 5), false, 5, 4, 3, null),
                Create(11, "Камера за входа", "GN-CAM-OUT1", 6.5, new DateTime(2023, 10, 10), true, 6, 3, 4, "Записва при движение."),
                Create(12, "Контролер за гаражна врата", "SH-GATE-0042", 5, new DateTime(2024, 6, 18), false, 6, 3, 3, null));
        }

        private static Device Create(int id, string name, string serial, double power, DateTime installedOn,
            bool isOn, int roomId, int categoryId, int manufacturerId, string? notes)
            => new Device
            {
                Id = id,
                Name = name,
                SerialNumber = serial,
                PowerWatts = power,
                InstalledOn = installedOn,
                IsOn = isOn,
                RoomId = roomId,
                CategoryId = categoryId,
                ManufacturerId = manufacturerId,
                Notes = notes
            };
    }
}