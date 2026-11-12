using Microsoft.AspNetCore.Mvc;
using SmartHome.Web.Common;
using SmartHome.Web.Infrastructure;
using SmartHome.Web.Services.Interfaces;
using SmartHome.Web.ViewModels.Devices;

namespace SmartHome.Web.Controllers
{
    public class DevicesController : Controller
    {
        private readonly IDeviceService deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(DeviceFilterViewModel filter)
        {
            var model = await deviceService.GetFilteredAsync(filter);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await deviceService.GetDetailsAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? roomId)
        {
            var model = new DeviceFormModel
            {
                InstalledOn = DateTime.Today,
                RoomId = roomId
            };

            await deviceService.LoadOptionsAsync(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeviceFormModel model)
        {
            ModelState.AddErrors(await deviceService.ValidateAsync(model, null));

            if (!ModelState.IsValid)
            {
                await deviceService.LoadOptionsAsync(model);
                return View(model);
            }

            int id = await deviceService.CreateAsync(model);
            TempData[TempDataKeys.Success] = $"Устройството „{model.Name.Trim()}“ беше добавено успешно.";

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await deviceService.GetForEditAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DeviceFormModel model)
        {
            ModelState.AddErrors(await deviceService.ValidateAsync(model, id));

            if (!ModelState.IsValid)
            {
                await deviceService.LoadOptionsAsync(model);
                return View(model);
            }

            bool updated = await deviceService.UpdateAsync(id, model);
            if (!updated)
            {
                return NotFound();
            }

            TempData[TempDataKeys.Success] = "Промените по устройството са запазени.";
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await deviceService.GetDetailsAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleted = await deviceService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            TempData[TempDataKeys.Success] = "Устройството беше изтрито.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(int id, string? returnUrl)
        {
            bool? isOn = await deviceService.ToggleAsync(id);
            if (isOn == null)
            {
                return NotFound();
            }

            TempData[TempDataKeys.Success] = isOn.Value ? "Устройството е включено." : "Устройството е изключено.";

           
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}