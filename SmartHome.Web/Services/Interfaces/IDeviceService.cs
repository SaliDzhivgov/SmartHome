using SmartHome.Web.ViewModels.Devices;

namespace SmartHome.Web.Services.Interfaces
{
    public interface IDeviceService
    {
        Task<DeviceFilterViewModel> GetFilteredAsync(DeviceFilterViewModel filter);

        Task<DeviceDetailsViewModel?> GetDetailsAsync(int id);

        Task<DeviceFormModel?> GetForEditAsync(int id);

        Task LoadOptionsAsync(DeviceFormModel model);

        Task<IDictionary<string, string>> ValidateAsync(DeviceFormModel model, int? deviceId);

        Task<int> CreateAsync(DeviceFormModel model);

        Task<bool> UpdateAsync(int id, DeviceFormModel model);

        Task<bool> DeleteAsync(int id);

        Task<bool?> ToggleAsync(int id);
    }
}