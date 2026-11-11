using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SmartHome.Web.Infrastructure;
using SmartHome.Web.ViewModels.Shared;
using static SmartHome.Web.Common.ErrorMessages;
using static SmartHome.Web.Common.ValidationConstants;

namespace SmartHome.Web.ViewModels.Devices
{
    // Модел за формите "Добави" и "Редактирай" устройство
    public class DeviceFormModel
    {
        [Display(Name = "Име")]
        [Required(ErrorMessage = Required)]
        [StringLength(DeviceNameMaxLength, MinimumLength = DeviceNameMinLength, ErrorMessage = LengthBetween)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Сериен номер")]
        [Required(ErrorMessage = Required)]
        [StringLength(DeviceSerialMaxLength, MinimumLength = DeviceSerialMinLength, ErrorMessage = LengthBetween)]
        [RegularExpression(DeviceSerialPattern, ErrorMessage = SerialFormat)]
        public string SerialNumber { get; set; } = string.Empty;

        [Display(Name = "Мощност (W)")]
        [Required(ErrorMessage = Required)]
        [Range(DevicePowerMin, DevicePowerMax, ErrorMessage = RangeBetween)]
        public double? PowerWatts { get; set; }

        [Display(Name = "Дата на инсталиране")]
        [Required(ErrorMessage = Required)]
        [DataType(DataType.Date)]
        [NotInFuture]
        public DateTime? InstalledOn { get; set; }

        [Display(Name = "Бележки")]
        [StringLength(DeviceNotesMaxLength, ErrorMessage = MaxLength)]
        public string? Notes { get; set; }

        [Display(Name = "Включено")]
        public bool IsOn { get; set; }

        [Display(Name = "Стая")]
        [Required(ErrorMessage = Required)]
        public int? RoomId { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = Required)]
        public int? CategoryId { get; set; }

        [Display(Name = "Производител")]
        [Required(ErrorMessage = Required)]
        public int? ManufacturerId { get; set; }

        [ValidateNever]
        public IEnumerable<SelectOptionViewModel> Rooms { get; set; } = new List<SelectOptionViewModel>();

        [ValidateNever]
        public IEnumerable<SelectOptionViewModel> Categories { get; set; } = new List<SelectOptionViewModel>();

        [ValidateNever]
        public IEnumerable<SelectOptionViewModel> Manufacturers { get; set; } = new List<SelectOptionViewModel>();
    }
}