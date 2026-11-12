using Microsoft.EntityFrameworkCore;
using SmartHome.Web.Common;
using SmartHome.Web.Data;
using SmartHome.Web.Data.Entities;
using SmartHome.Web.Services.Interfaces;
using SmartHome.Web.Services.Mapping;
using SmartHome.Web.ViewModels.Devices;
using SmartHome.Web.ViewModels.Shared;

namespace SmartHome.Web.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly SmartHomeDbContext dbContext;

        public DeviceService(SmartHomeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<DeviceFilterViewModel> GetFilteredAsync(DeviceFilterViewModel filter)
        {
            IQueryable<Device> query = dbContext.Devices.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                string term = filter.SearchTerm.Trim();
                query = query.Where(d => d.Name.Contains(term) || d.SerialNumber.Contains(term));
            }

            if (filter.RoomId.HasValue)
            {
                query = query.Where(d => d.RoomId == filter.RoomId.Value);
            }

            if (filter.CategoryId.HasValue)
            {
                query = query.Where(d => d.CategoryId == filter.CategoryId.Value);
            }

            if (filter.Status == DeviceStatusFilter.On)
            {
                query = query.Where(d => d.IsOn);
            }
            else if (filter.Status == DeviceStatusFilter.Off)
            {
                query = query.Where(d => !d.IsOn);
            }

            filter.Devices = await query
                .OrderBy(d => d.Room.Name)
                .ThenBy(d => d.Name)
                .Select(DeviceProjections.ToCard)
                .ToListAsync();

            filter.Rooms = await GetRoomOptionsAsync();
            filter.Categories = await GetCategoryOptionsAsync();

            return filter;
        }

        public async Task<DeviceDetailsViewModel?> GetDetailsAsync(int id)
        {
            return await dbContext.Devices
                .AsNoTracking()
                .Where(d => d.Id == id)
                .Select(d => new DeviceDetailsViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    SerialNumber = d.SerialNumber,
                    PowerWatts = d.PowerWatts,
                    InstalledOn = d.InstalledOn,
                    Notes = d.Notes,
                    IsOn = d.IsOn,
                    RoomId = d.RoomId,
                    RoomName = d.Room.Name,
                    CategoryName = d.Category.Name,
                    CategoryIcon = d.Category.Icon,
                    ManufacturerName = d.Manufacturer.Name,
                    ManufacturerWebsite = d.Manufacturer.Website
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DeviceFormModel?> GetForEditAsync(int id)
        {
            var model = await dbContext.Devices
                .AsNoTracking()
                .Where(d => d.Id == id)
                .Select(d => new DeviceFormModel
                {
                    Name = d.Name,
                    SerialNumber = d.SerialNumber,
                    PowerWatts = d.PowerWatts,
                    InstalledOn = d.InstalledOn,
                    Notes = d.Notes,
                    IsOn = d.IsOn,
                    RoomId = d.RoomId,
                    CategoryId = d.CategoryId,
                    ManufacturerId = d.ManufacturerId
                })
                .FirstOrDefaultAsync();

            if (model != null)
            {
                await LoadOptionsAsync(model);
            }

            return model;
        }

        public async Task LoadOptionsAsync(DeviceFormModel model)
        {
            model.Rooms = await GetRoomOptionsAsync();
            model.Categories = await GetCategoryOptionsAsync();
            model.Manufacturers = await dbContext.Manufacturers
                .AsNoTracking()
                .OrderBy(m => m.Name)
                .Select(m => new SelectOptionViewModel { Id = m.Id, Name = m.Name })
                .ToListAsync();
        }

        public async Task<IDictionary<string, string>> ValidateAsync(DeviceFormModel model, int? deviceId)
        {
            var errors = new Dictionary<string, string>();
            string serial = model.SerialNumber.Trim();

            bool serialTaken = await dbContext.Devices
                .AnyAsync(d => d.SerialNumber == serial && (deviceId == null || d.Id != deviceId));
            if (serialTaken)
            {
                errors[nameof(DeviceFormModel.SerialNumber)] = ErrorMessages.DuplicateSerialNumber;
            }

            if (model.RoomId.HasValue && !await dbContext.Rooms.AnyAsync(r => r.Id == model.RoomId))
            {
                errors[nameof(DeviceFormModel.RoomId)] = ErrorMessages.InvalidRoom;
            }

            if (model.CategoryId.HasValue && !await dbContext.Categories.AnyAsync(c => c.Id == model.CategoryId))
            {
                errors[nameof(DeviceFormModel.CategoryId)] = ErrorMessages.InvalidCategory;
            }

            if (model.ManufacturerId.HasValue && !await dbContext.Manufacturers.AnyAsync(m => m.Id == model.ManufacturerId))
            {
                errors[nameof(DeviceFormModel.ManufacturerId)] = ErrorMessages.InvalidManufacturer;
            }

            return errors;
        }

        public async Task<int> CreateAsync(DeviceFormModel model)
        {
            var device = new Device();
            MapToEntity(model, device);

            await dbContext.Devices.AddAsync(device);
            await dbContext.SaveChangesAsync();

            return device.Id;
        }

        public async Task<bool> UpdateAsync(int id, DeviceFormModel model)
        {
            var device = await dbContext.Devices.FindAsync(id);
            if (device == null)
            {
                return false;
            }

            MapToEntity(model, device);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var device = await dbContext.Devices.FindAsync(id);
            if (device == null)
            {
                return false;
            }

            dbContext.Devices.Remove(device);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool?> ToggleAsync(int id)
        {
            var device = await dbContext.Devices.FindAsync(id);
            if (device == null)
            {
                return null;
            }

            device.IsOn = !device.IsOn;
            await dbContext.SaveChangesAsync();
            return device.IsOn;
        }

        private static void MapToEntity(DeviceFormModel model, Device device)
        {
            device.Name = model.Name.Trim();
            device.SerialNumber = model.SerialNumber.Trim();
            device.PowerWatts = model.PowerWatts!.Value;
            device.InstalledOn = model.InstalledOn!.Value.Date;
            device.Notes = model.Notes?.Trim();
            device.IsOn = model.IsOn;
            device.RoomId = model.RoomId!.Value;
            device.CategoryId = model.CategoryId!.Value;
            device.ManufacturerId = model.ManufacturerId!.Value;
        }

        private async Task<IEnumerable<SelectOptionViewModel>> GetRoomOptionsAsync()
        {
            return await dbContext.Rooms
                .AsNoTracking()
                .OrderBy(r => r.Name)
                .Select(r => new SelectOptionViewModel { Id = r.Id, Name = r.Name })
                .ToListAsync();
        }

        private async Task<IEnumerable<SelectOptionViewModel>> GetCategoryOptionsAsync()
        {
            return await dbContext.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => new SelectOptionViewModel { Id = c.Id, Name = c.Name })
                .ToListAsync();
        }
    }
}