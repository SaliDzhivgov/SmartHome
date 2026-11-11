using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartHome.Web.ViewModels.Shared;

namespace SmartHome.Web.ViewModels.Devices
{
    public enum DeviceStatusFilter
    {
        All,
        On,
        Off
    }

    // Параметрите на филтъра идват от адреса (?SearchTerm=...&RoomId=...), а резултатът се връща в Devices
    public class DeviceFilterViewModel
    {
        [Display(Name = "Търсене")]
        public string? SearchTerm { get; set; }

        [Display(Name = "Стая")]
        public int? RoomId { get; set; }

        [Display(Name = "Категория")]
        public int? CategoryId { get; set; }

        [Display(Name = "Състояние")]
        public DeviceStatusFilter Status { get; set; } = DeviceStatusFilter.All;

        [BindNever]
        public IEnumerable<DeviceCardViewModel> Devices { get; set; } = new List<DeviceCardViewModel>();

        [BindNever]
        public IEnumerable<SelectOptionViewModel> Rooms { get; set; } = new List<SelectOptionViewModel>();

        [BindNever]
        public IEnumerable<SelectOptionViewModel> Categories { get; set; } = new List<SelectOptionViewModel>();

        public bool HasActiveFilters =>
            !string.IsNullOrWhiteSpace(SearchTerm) || RoomId.HasValue || CategoryId.HasValue || Status != DeviceStatusFilter.All;
    }
}