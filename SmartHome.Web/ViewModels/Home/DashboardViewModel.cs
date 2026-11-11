using SmartHome.Web.ViewModels.Devices;
using SmartHome.Web.ViewModels.Rooms;

namespace SmartHome.Web.ViewModels.Home
{
    public class DashboardViewModel
    {
        public int RoomsCount { get; set; }

        public int DevicesCount { get; set; }

        public int ActiveDevicesCount { get; set; }

        public double ActivePowerWatts { get; set; }

        public IEnumerable<RoomListItemViewModel> Rooms { get; set; } = new List<RoomListItemViewModel>();

        public IEnumerable<DeviceCardViewModel> RecentDevices { get; set; } = new List<DeviceCardViewModel>();
    }
}