using System.ComponentModel.DataAnnotations;
using static SmartHome.Web.Common.ValidationConstants;

namespace SmartHome.Web.Data.Entities
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(RoomNameMaxLength)]
        public string Name { get; set; } = null!;

        public int Floor { get; set; }

        [MaxLength(RoomDescriptionMaxLength)]
        public string? Description { get; set; }

        public ICollection<Device> Devices { get; set; } = new HashSet<Device>();
    }
}