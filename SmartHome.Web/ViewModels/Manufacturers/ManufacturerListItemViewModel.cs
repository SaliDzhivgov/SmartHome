namespace SmartHome.Web.ViewModels.Manufacturers
{
    public class ManufacturerListItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string? Website { get; set; }

        public int DeviceCount { get; set; }
    }
}