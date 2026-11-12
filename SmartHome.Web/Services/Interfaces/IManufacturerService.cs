using SmartHome.Web.Common;
using SmartHome.Web.ViewModels.Manufacturers;
using SmartHome.Web.ViewModels.Shared;

namespace SmartHome.Web.Services.Interfaces
{
    public interface IManufacturerService
    {
        Task<IEnumerable<ManufacturerListItemViewModel>> GetAllAsync();

        Task<ManufacturerFormModel?> GetForEditAsync(int id);

        Task<DeleteViewModel?> GetForDeleteAsync(int id);

        Task<IDictionary<string, string>> ValidateAsync(ManufacturerFormModel model, int? manufacturerId);

        Task<int> CreateAsync(ManufacturerFormModel model);

        Task<bool> UpdateAsync(int id, ManufacturerFormModel model);

        Task<DeleteResult> DeleteAsync(int id);
    }
}