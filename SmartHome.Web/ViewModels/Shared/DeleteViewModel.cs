namespace SmartHome.Web.ViewModels.Shared
{
    // Използва се от страниците за изтриване на стаи, категории и производители
    public class DeleteViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int DeviceCount { get; set; }

        public bool CanDelete => DeviceCount == 0;
    }
}