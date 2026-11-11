using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SmartHome.Web.Common.ValidationConstants;

namespace SmartHome.Web.Data.Entities
{
    public class Device
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DeviceNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DeviceSerialMaxLength)]
        public string SerialNumber { get; set; } = null!;

        public double PowerWatts { get; set; }

        public DateTime InstalledOn { get; set; }

        [MaxLength(DeviceNotesMaxLength)]
        public string? Notes { get; set; }

        public bool IsOn { get; set; }

        [ForeignKey(nameof(Room))]
        public int RoomId { get; set; }

        public Room Room { get; set; } = null!;

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; } = null!;
    }
}