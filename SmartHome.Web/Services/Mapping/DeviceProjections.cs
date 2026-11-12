using System.Linq.Expressions;
using SmartHome.Web.Data.Entities;
using SmartHome.Web.ViewModels.Devices;

namespace SmartHome.Web.Services.Mapping
{
    
    public static class DeviceProjections
    {
        public static readonly Expression<Func<Device, DeviceCardViewModel>> ToCard = d => new DeviceCardViewModel
        {
            Id = d.Id,
            Name = d.Name,
            SerialNumber = d.SerialNumber,
            RoomName = d.Room.Name,
            CategoryName = d.Category.Name,
            CategoryIcon = d.Category.Icon,
            ManufacturerName = d.Manufacturer.Name,
            PowerWatts = d.PowerWatts,
            IsOn = d.IsOn
        };
    }
}