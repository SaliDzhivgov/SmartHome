namespace SmartHome.Web.ViewModels.Devices
{
    public class DeviceDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string SerialNumber { get; set; } = null!;

        public double PowerWatts { get; set; }

        public DateTime InstalledOn { get; set; }

        public string? Notes { get; set; }

        public bool IsOn { get; set; }

        public int RoomId { get; set; }

        public string RoomName { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public string CategoryIcon { get; set; } = null!;

        public string ManufacturerName { get; set; } = null!;

        public string? ManufacturerWebsite { get; set; }
    }
}