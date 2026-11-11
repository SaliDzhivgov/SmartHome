using System.ComponentModel.DataAnnotations;
using static SmartHome.Web.Common.ValidationConstants;

namespace SmartHome.Web.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(CategoryIconMaxLength)]
        public string Icon { get; set; } = null!;

        [MaxLength(CategoryDescriptionMaxLength)]
        public string? Description { get; set; }

        public ICollection<Device> Devices { get; set; } = new HashSet<Device>();
    }
}