using SmartHome.Web.ViewModels.Devices;

namespace SmartHome.Web.ViewModels.Rooms
{
    public class RoomDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Floor { get; set; }

        public string? Description { get; set; }

        public IEnumerable<DeviceCardViewModel> Devices { get; set; } = new List<DeviceCardViewModel>();

        public double ActivePowerWatts => Devices.Where(d => d.IsOn).Sum(d => d.PowerWatts);
    }
}