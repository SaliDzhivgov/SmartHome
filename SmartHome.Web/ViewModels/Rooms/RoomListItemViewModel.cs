namespace SmartHome.Web.ViewModels.Rooms
{
    public class RoomListItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Floor { get; set; }

        public string? Description { get; set; }

        public int DeviceCount { get; set; }

        public int ActiveDeviceCount { get; set; }

        public double ActivePowerWatts { get; set; }
    }
}