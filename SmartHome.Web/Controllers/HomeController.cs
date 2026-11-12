using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Web.Services.Interfaces;
using SmartHome.Web.ViewModels.Shared;

namespace SmartHome.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDashboardService dashboardService;

        public HomeController(IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await dashboardService.GetDashboardAsync();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}