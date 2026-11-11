using System.ComponentModel.DataAnnotations;
using static SmartHome.Web.Common.ErrorMessages;
using static SmartHome.Web.Common.ValidationConstants;

namespace SmartHome.Web.ViewModels.Manufacturers
{
    public class ManufacturerFormModel
    {
        [Display(Name = "Име на производителя")]
        [Required(ErrorMessage = Required)]
        [StringLength(ManufacturerNameMaxLength, MinimumLength = ManufacturerNameMinLength, ErrorMessage = LengthBetween)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Държава")]
        [Required(ErrorMessage = Required)]
        [StringLength(ManufacturerCountryMaxLength, MinimumLength = ManufacturerCountryMinLength, ErrorMessage = LengthBetween)]
        public string Country { get; set; } = string.Empty;

        [Display(Name = "Уебсайт")]
        [StringLength(ManufacturerWebsiteMaxLength, ErrorMessage = MaxLength)]
        [Url(ErrorMessage = InvalidUrl)]
        public string? Website { get; set; }
    }
}