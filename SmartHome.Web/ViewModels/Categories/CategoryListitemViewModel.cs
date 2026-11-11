namespace SmartHome.Web.ViewModels.Categories
{
    public class CategoryListItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Icon { get; set; } = null!;

        public string? Description { get; set; }

        public int DeviceCount { get; set; }
    }
}