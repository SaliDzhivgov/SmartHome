using Microsoft.AspNetCore.Mvc;
using SmartHome.Web.Common;
using SmartHome.Web.Infrastructure;
using SmartHome.Web.Services.Interfaces;
using SmartHome.Web.ViewModels.Manufacturers;

namespace SmartHome.Web.Controllers
{
    public class ManufacturersController : Controller
    {
        private readonly IManufacturerService manufacturerService;

        public ManufacturersController(IManufacturerService manufacturerService)
        {
            this.manufacturerService = manufacturerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await manufacturerService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ManufacturerFormModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManufacturerFormModel model)
        {
            ModelState.AddErrors(await manufacturerService.ValidateAsync(model, null));

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await manufacturerService.CreateAsync(model);
            TempData[TempDataKeys.Success] = $"Производителят „{model.Name.Trim()}“ беше добавен.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await manufacturerService.GetForEditAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ManufacturerFormModel model)
        {
            ModelState.AddErrors(await manufacturerService.ValidateAsync(model, id));

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool updated = await manufacturerService.UpdateAsync(id, model);
            if (!updated)
            {
                return NotFound();
            }

            TempData[TempDataKeys.Success] = "Промените по производителя са запазени.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await manufacturerService.GetForDeleteAsync(id);
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
            var result = await manufacturerService.DeleteAsync(id);

            switch (result)
            {
                case DeleteResult.NotFound:
                    return NotFound();
                case DeleteResult.HasDevices:
                    TempData[TempDataKeys.Error] = "Производителят не може да бъде изтрит, защото има устройства от него.";
                    return RedirectToAction(nameof(Delete), new { id });
                default:
                    TempData[TempDataKeys.Success] = "Производителят беше изтрит.";
                    return RedirectToAction(nameof(Index));
            }
        }
    }
}