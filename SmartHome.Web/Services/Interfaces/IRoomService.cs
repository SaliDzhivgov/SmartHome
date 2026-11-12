using SmartHome.Web.Common;
using SmartHome.Web.ViewModels.Rooms;
using SmartHome.Web.ViewModels.Shared;

namespace SmartHome.Web.Services.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomListItemViewModel>> GetAllAsync();

        Task<RoomDetailsViewModel?> GetDetailsAsync(int id);

        Task<RoomFormModel?> GetForEditAsync(int id);

        Task<DeleteViewModel?> GetForDeleteAsync(int id);

        Task<IDictionary<string, string>> ValidateAsync(RoomFormModel model, int? roomId);

        Task<int> CreateAsync(RoomFormModel model);

        Task<bool> UpdateAsync(int id, RoomFormModel model);

        Task<DeleteResult> DeleteAsync(int id);
    }
}