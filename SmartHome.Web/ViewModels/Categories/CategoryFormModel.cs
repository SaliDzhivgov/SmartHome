using System.ComponentModel.DataAnnotations;
using static SmartHome.Web.Common.ErrorMessages;
using static SmartHome.Web.Common.ValidationConstants;

namespace SmartHome.Web.ViewModels.Categories
{
    public class CategoryFormModel
    {
        [Display(Name = "Име на категорията")]
        [Required(ErrorMessage = Required)]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength, ErrorMessage = LengthBetween)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Икона")]
        [Required(ErrorMessage = Required)]
        [StringLength(CategoryIconMaxLength, ErrorMessage = MaxLength)]
        public string Icon { get; set; } = string.Empty;

        [Display(Name = "Описание")]
        [StringLength(CategoryDescriptionMaxLength, ErrorMessage = MaxLength)]
        public string? Description { get; set; }
    }
}