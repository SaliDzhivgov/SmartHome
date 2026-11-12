using Microsoft.AspNetCore.Mvc;
using SmartHome.Web.Common;
using SmartHome.Web.Infrastructure;
using SmartHome.Web.Services.Interfaces;
using SmartHome.Web.ViewModels.Rooms;

namespace SmartHome.Web.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomService roomService;

        public RoomsController(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await roomService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await roomService.GetDetailsAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new RoomFormModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomFormModel model)
        {
            ModelState.AddErrors(await roomService.ValidateAsync(model, null));

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int id = await roomService.CreateAsync(model);
            TempData[TempDataKeys.Success] = $"Стаята „{model.Name.Trim()}“ беше добавена.";

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await roomService.GetForEditAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomFormModel model)
        {
            ModelState.AddErrors(await roomService.ValidateAsync(model, id));

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool updated = await roomService.UpdateAsync(id, model);
            if (!updated)
            {
                return NotFound();
            }

            TempData[TempDataKeys.Success] = "Промените по стаята са запазени.";
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await roomService.GetForDeleteAsync(id);
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
            var result = await roomService.DeleteAsync(id);

            switch (result)
            {
                case DeleteResult.NotFound:
                    return NotFound();
                case DeleteResult.HasDevices:
                    TempData[TempDataKeys.Error] = "Стаята не може да бъде изтрита, защото в нея има устройства. Първо ги преместете или изтрийте.";
                    return RedirectToAction(nameof(Delete), new { id });
                default:
                    TempData[TempDataKeys.Success] = "Стаята беше изтрита.";
                    return RedirectToAction(nameof(Index));
            }
        }
    }
}