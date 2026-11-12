using SmartHome.Web.Common;
using SmartHome.Web.ViewModels.Categories;
using SmartHome.Web.ViewModels.Shared;

namespace SmartHome.Web.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryListItemViewModel>> GetAllAsync();

        Task<CategoryFormModel?> GetForEditAsync(int id);

        Task<DeleteViewModel?> GetForDeleteAsync(int id);

        Task<IDictionary<string, string>> ValidateAsync(CategoryFormModel model, int? categoryId);

        Task<int> CreateAsync(CategoryFormModel model);

        Task<bool> UpdateAsync(int id, CategoryFormModel model);

        Task<DeleteResult> DeleteAsync(int id);
    }
}