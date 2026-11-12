using Microsoft.EntityFrameworkCore;
using SmartHome.Web.Common;
using SmartHome.Web.Data;
using SmartHome.Web.Data.Entities;
using SmartHome.Web.Services.Interfaces;
using SmartHome.Web.Services.Mapping;
using SmartHome.Web.ViewModels.Rooms;
using SmartHome.Web.ViewModels.Shared;

namespace SmartHome.Web.Services
{
    public class RoomService : IRoomService
    {
        private readonly SmartHomeDbContext dbContext;

        public RoomService(SmartHomeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<RoomListItemViewModel>> GetAllAsync()
        {
            return await dbContext.Rooms
                .AsNoTracking()
                .OrderBy(r => r.Floor)
                .ThenBy(r => r.Name)
                .Select(r => new RoomListItemViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Floor = r.Floor,
                    Description = r.Description,
                    DeviceCount = r.Devices.Count,
                    ActiveDeviceCount = r.Devices.Count(d => d.IsOn),
                    ActivePowerWatts = r.Devices.Where(d => d.IsOn).Sum(d => d.PowerWatts)
                })
                .ToListAsync();
        }

        public async Task<RoomDetailsViewModel?> GetDetailsAsync(int id)
        {
            var room = await dbContext.Rooms
                .AsNoTracking()
                .Where(r => r.Id == id)
                .Select(r => new RoomDetailsViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Floor = r.Floor,
                    Description = r.Description
                })
                .FirstOrDefaultAsync();

            if (room == null)
            {
                return null;
            }

            room.Devices = await dbContext.Devices
                .AsNoTracking()
                .Where(d => d.RoomId == id)
                .OrderBy(d => d.Name)
                .Select(DeviceProjections.ToCard)
                .ToListAsync();

            return room;
        }

        public async Task<RoomFormModel?> GetForEditAsync(int id)
        {
            return await dbContext.Rooms
                .AsNoTracking()
                .Where(r => r.Id == id)
                .Select(r => new RoomFormModel
                {
                    Name = r.Name,
                    Floor = r.Floor,
                    Description = r.Description
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DeleteViewModel?> GetForDeleteAsync(int id)
        {
            return await dbContext.Rooms
                .AsNoTracking()
                .Where(r => r.Id == id)
                .Select(r => new DeleteViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    DeviceCount = r.Devices.Count
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IDictionary<string, string>> ValidateAsync(RoomFormModel model, int? roomId)
        {
            var errors = new Dictionary<string, string>();
            string name = model.Name.Trim();

            bool nameTaken = await dbContext.Rooms
                .AnyAsync(r => r.Name == name && (roomId == null || r.Id != roomId));

            if (nameTaken)
            {
                errors[nameof(RoomFormModel.Name)] = ErrorMessages.DuplicateRoomName;
            }

            return errors;
        }

        public async Task<int> CreateAsync(RoomFormModel model)
        {
            var room = new Room
            {
                Name = model.Name.Trim(),
                Floor = model.Floor!.Value,
                Description = model.Description?.Trim()
            };

            await dbContext.Rooms.AddAsync(room);
            await dbContext.SaveChangesAsync();

            return room.Id;
        }

        public async Task<bool> UpdateAsync(int id, RoomFormModel model)
        {
            var room = await dbContext.Rooms.FindAsync(id);
            if (room == null)
            {
                return false;
            }

            room.Name = model.Name.Trim();
            room.Floor = model.Floor!.Value;
            room.Description = model.Description?.Trim();

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<DeleteResult> DeleteAsync(int id)
        {
            var room = await dbContext.Rooms.FindAsync(id);
            if (room == null)
            {
                return DeleteResult.NotFound;
            }

            bool hasDevices = await dbContext.Devices.AnyAsync(d => d.RoomId == id);
            if (hasDevices)
            {
                return DeleteResult.HasDevices;
            }

            dbContext.Rooms.Remove(room);
            await dbContext.SaveChangesAsync();
            return DeleteResult.Deleted;
        }
    }
}