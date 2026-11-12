using Microsoft.AspNetCore.Mvc;
using SmartHome.Web.Common;
using SmartHome.Web.Infrastructure;
using SmartHome.Web.Services.Interfaces;
using SmartHome.Web.ViewModels.Categories;

namespace SmartHome.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await categoryService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CategoryFormModel { Icon = CategoryIcons.All.Keys.First() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryFormModel model)
        {
            ModelState.AddErrors(await categoryService.ValidateAsync(model, null));

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await categoryService.CreateAsync(model);
            TempData[TempDataKeys.Success] = $"Категорията „{model.Name.Trim()}“ беше добавена.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await categoryService.GetForEditAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryFormModel model)
        {
            ModelState.AddErrors(await categoryService.ValidateAsync(model, id));

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool updated = await categoryService.UpdateAsync(id, model);
            if (!updated)
            {
                return NotFound();
            }

            TempData[TempDataKeys.Success] = "Промените по категорията са запазени.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await categoryService.GetForDeleteAsync(id);
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
            var result = await categoryService.DeleteAsync(id);

            switch (result)
            {
                case DeleteResult.NotFound:
                    return NotFound();
                case DeleteResult.HasDevices:
                    TempData[TempDataKeys.Error] = "Категорията не може да бъде изтрита, защото към нея има устройства.";
                    return RedirectToAction(nameof(Delete), new { id });
                default:
                    TempData[TempDataKeys.Success] = "Категорията беше изтрита.";
                    return RedirectToAction(nameof(Index));
            }
        }
    }
}