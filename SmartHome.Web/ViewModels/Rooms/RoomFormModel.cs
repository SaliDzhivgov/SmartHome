using System.ComponentModel.DataAnnotations;
using static SmartHome.Web.Common.ErrorMessages;
using static SmartHome.Web.Common.ValidationConstants;

namespace SmartHome.Web.ViewModels.Rooms
{
    public class RoomFormModel
    {
        [Display(Name = "Име на стаята")]
        [Required(ErrorMessage = Required)]
        [StringLength(RoomNameMaxLength, MinimumLength = RoomNameMinLength, ErrorMessage = LengthBetween)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Етаж")]
        [Required(ErrorMessage = Required)]
        [Range(RoomFloorMin, RoomFloorMax, ErrorMessage = RangeBetween)]
        public int? Floor { get; set; }

        [Display(Name = "Описание")]
        [StringLength(RoomDescriptionMaxLength, ErrorMessage = MaxLength)]
        public string? Description { get; set; }
    }
}