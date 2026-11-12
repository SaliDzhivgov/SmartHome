using Microsoft.EntityFrameworkCore;
using SmartHome.Web.Common;
using SmartHome.Web.Data;
using SmartHome.Web.Data.Entities;
using SmartHome.Web.Services.Interfaces;
using SmartHome.Web.ViewModels.Manufacturers;
using SmartHome.Web.ViewModels.Shared;

namespace SmartHome.Web.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly SmartHomeDbContext dbContext;

        public ManufacturerService(SmartHomeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ManufacturerListItemViewModel>> GetAllAsync()
        {
            return await dbContext.Manufacturers
                .AsNoTracking()
                .OrderBy(m => m.Name)
                .Select(m => new ManufacturerListItemViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Country = m.Country,
                    Website = m.Website,
                    DeviceCount = m.Devices.Count
                })
                .ToListAsync();
        }

        public async Task<ManufacturerFormModel?> GetForEditAsync(int id)
        {
            return await dbContext.Manufacturers
                .AsNoTracking()
                .Where(m => m.Id == id)
                .Select(m => new ManufacturerFormModel
                {
                    Name = m.Name,
                    Country = m.Country,
                    Website = m.Website
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DeleteViewModel?> GetForDeleteAsync(int id)
        {
            return await dbContext.Manufacturers
                .AsNoTracking()
                .Where(m => m.Id == id)
                .Select(m => new DeleteViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    DeviceCount = m.Devices.Count
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IDictionary<string, string>> ValidateAsync(ManufacturerFormModel model, int? manufacturerId)
        {
            var errors = new Dictionary<string, string>();
            string name = model.Name.Trim();

            bool nameTaken = await dbContext.Manufacturers
                .AnyAsync(m => m.Name == name && (manufacturerId == null || m.Id != manufacturerId));

            if (nameTaken)
            {
                errors[nameof(ManufacturerFormModel.Name)] = ErrorMessages.DuplicateManufacturerName;
            }

            return errors;
        }

        public async Task<int> CreateAsync(ManufacturerFormModel model)
        {
            var manufacturer = new Manufacturer
            {
                Name = model.Name.Trim(),
                Country = model.Country.Trim(),
                Website = model.Website?.Trim()
            };

            await dbContext.Manufacturers.AddAsync(manufacturer);
            await dbContext.SaveChangesAsync();

            return manufacturer.Id;
        }

        public async Task<bool> UpdateAsync(int id, ManufacturerFormModel model)
        {
            var manufacturer = await dbContext.Manufacturers.FindAsync(id);
            if (manufacturer == null)
            {
                return false;
            }

            manufacturer.Name = model.Name.Trim();
            manufacturer.Country = model.Country.Trim();
            manufacturer.Website = model.Website?.Trim();

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<DeleteResult> DeleteAsync(int id)
        {
            var manufacturer = await dbContext.Manufacturers.FindAsync(id);
            if (manufacturer == null)
            {
                return DeleteResult.NotFound;
            }

            bool hasDevices = await dbContext.Devices.AnyAsync(d => d.ManufacturerId == id);
            if (hasDevices)
            {
                return DeleteResult.HasDevices;
            }

            dbContext.Manufacturers.Remove(manufacturer);
            await dbContext.SaveChangesAsync();
            return DeleteResult.Deleted;
        }
    }
}