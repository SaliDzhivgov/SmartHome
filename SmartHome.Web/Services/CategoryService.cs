using Microsoft.EntityFrameworkCore;
using SmartHome.Web.Common;
using SmartHome.Web.Data;
using SmartHome.Web.Data.Entities;
using SmartHome.Web.Services.Interfaces;
using SmartHome.Web.ViewModels.Categories;
using SmartHome.Web.ViewModels.Shared;

namespace SmartHome.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly SmartHomeDbContext dbContext;

        public CategoryService(SmartHomeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CategoryListItemViewModel>> GetAllAsync()
        {
            return await dbContext.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => new CategoryListItemViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Icon = c.Icon,
                    Description = c.Description,
                    DeviceCount = c.Devices.Count
                })
                .ToListAsync();
        }

        public async Task<CategoryFormModel?> GetForEditAsync(int id)
        {
            return await dbContext.Categories
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CategoryFormModel
                {
                    Name = c.Name,
                    Icon = c.Icon,
                    Description = c.Description
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DeleteViewModel?> GetForDeleteAsync(int id)
        {
            return await dbContext.Categories
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new DeleteViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    DeviceCount = c.Devices.Count
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IDictionary<string, string>> ValidateAsync(CategoryFormModel model, int? categoryId)
        {
            var errors = new Dictionary<string, string>();
            string name = model.Name.Trim();

            bool nameTaken = await dbContext.Categories
                .AnyAsync(c => c.Name == name && (categoryId == null || c.Id != categoryId));

            if (nameTaken)
            {
                errors[nameof(CategoryFormModel.Name)] = ErrorMessages.DuplicateCategoryName;
            }

            if (!CategoryIcons.IsValid(model.Icon))
            {
                errors[nameof(CategoryFormModel.Icon)] = ErrorMessages.InvalidIcon;
            }

            return errors;
        }

        public async Task<int> CreateAsync(CategoryFormModel model)
        {
            var category = new Category
            {
                Name = model.Name.Trim(),
                Icon = model.Icon,
                Description = model.Description?.Trim()
            };

            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category.Id;
        }

        public async Task<bool> UpdateAsync(int id, CategoryFormModel model)
        {
            var category = await dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }

            category.Name = model.Name.Trim();
            category.Icon = model.Icon;
            category.Description = model.Description?.Trim();

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<DeleteResult> DeleteAsync(int id)
        {
            var category = await dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                return DeleteResult.NotFound;
            }

            bool hasDevices = await dbContext.Devices.AnyAsync(d => d.CategoryId == id);
            if (hasDevices)
            {
                return DeleteResult.HasDevices;
            }

            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();
            return DeleteResult.Deleted;
        }
    }
}