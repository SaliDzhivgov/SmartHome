namespace SmartHome.Web.ViewModels.Devices
{
    public class DeviceCardViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string SerialNumber { get; set; } = null!;

        public string RoomName { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public string CategoryIcon { get; set; } = null!;

        public string ManufacturerName { get; set; } = null!;

        public double PowerWatts { get; set; }

        public bool IsOn { get; set; }
    }
}