using System.ComponentModel.DataAnnotations;
using static SmartHome.Web.Common.ValidationConstants;

namespace SmartHome.Web.Data.Entities
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ManufacturerNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(ManufacturerCountryMaxLength)]
        public string Country { get; set; } = null!;

        [MaxLength(ManufacturerWebsiteMaxLength)]
        public string? Website { get; set; }

        public ICollection<Device> Devices { get; set; } = new HashSet<Device>();
    }
}