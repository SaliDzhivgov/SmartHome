using SmartHome.Web.ViewModels.Home;

namespace SmartHome.Web.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardAsync();
    }
}