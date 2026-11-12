using Microsoft.EntityFrameworkCore;
using SmartHome.Web.Data;
using SmartHome.Web.Services.Interfaces;
using SmartHome.Web.Services.Mapping;
using SmartHome.Web.ViewModels.Home;

namespace SmartHome.Web.Services
{
    public class DashboardService : IDashboardService
    {
        private const int RecentDevicesCount = 4;

        private readonly SmartHomeDbContext dbContext;
        private readonly IRoomService roomService;

        public DashboardService(SmartHomeDbContext dbContext, IRoomService roomService)
        {
            this.dbContext = dbContext;
            this.roomService = roomService;
        }

        public async Task<DashboardViewModel> GetDashboardAsync()
        {
            var rooms = (await roomService.GetAllAsync()).ToList();

            return new DashboardViewModel
            {
                RoomsCount = rooms.Count,
                DevicesCount = rooms.Sum(r => r.DeviceCount),
                ActiveDevicesCount = rooms.Sum(r => r.ActiveDeviceCount),
                ActivePowerWatts = rooms.Sum(r => r.ActivePowerWatts),
                Rooms = rooms,
                RecentDevices = await dbContext.Devices
                    .AsNoTracking()
                    .OrderByDescending(d => d.InstalledOn)
                    .ThenByDescending(d => d.Id)
                    .Take(RecentDevicesCount)
                    .Select(DeviceProjections.ToCard)
                    .ToListAsync()
            };
        }
    }
}