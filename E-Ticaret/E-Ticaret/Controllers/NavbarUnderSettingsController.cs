using Core.EncapsulationDtos;
using Core.Model;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Controllers
{
    public class NavbarUnderSettingsController : Controller
    {
        private readonly IService<NavbarCategory> _categoryService;

        private readonly IService<NavbarUnderCategory> _navbarUnderCategoryService;

        public NavbarUnderSettingsController(IService<NavbarCategory> categoryService, IService<NavbarUnderCategory> navbarUnderCategoryService)
        {
            _categoryService = categoryService;
            _navbarUnderCategoryService = navbarUnderCategoryService;
        }

        public async Task<IActionResult> IndexNavbarUnderCategory()
        {
            return View(await _navbarUnderCategoryService.GetAllAsync());
        }

        public async Task<IActionResult> SaveNavbarUnderCategory()
        {

            var navbarCategory = await _categoryService.GetAllAsync();

            var model = new HomeSaveEncapsulationModel
            {

                navbarCategories = (List<NavbarCategory>)navbarCategory,




            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveNavbarUnderCategory(HomeSaveEncapsulationModel homeSaveEncapsulationModel)
        {
            homeSaveEncapsulationModel.navbarUnderCategories.Id = Guid.NewGuid().ToString();

            await _navbarUnderCategoryService.AddAsync(homeSaveEncapsulationModel.navbarUnderCategories);

            return RedirectToAction("IndexNavbarUnderCategory", "NavbarUnderSettings");
        }

        public async Task<IActionResult> UpdateNavbarUnderCategory(string id)
        {
            var navbarUnderCategory = await _navbarUnderCategoryService.GetByIdAsync(id);

            var navbarCategory = await _categoryService.GetAllAsync();

            var model = new HomeSaveEncapsulationModel
            {

                navbarCategories = (List<NavbarCategory>)navbarCategory,

                navbarUnderCategories = navbarUnderCategory


            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNavbarUnderCategory(string id, HomeSaveEncapsulationModel  homeSaveEncapsulationModel)
        {
            var model = await _navbarUnderCategoryService.GetByIdAsync(id);

            model.CategoryName = homeSaveEncapsulationModel.navbarUnderCategories.CategoryName;

            model.UnderCategoryName = homeSaveEncapsulationModel.navbarUnderCategories.UnderCategoryName;

            _navbarUnderCategoryService.Update(model);

            return RedirectToAction("IndexNavbarUnderCategory", "NavbarUnderSettings");
        }

        public async Task<IActionResult> RemoveNavbarUnderCategory(string id)
        {
            var model = await _navbarUnderCategoryService.GetByIdAsync(id);

            _navbarUnderCategoryService?.Remove(model);

            return RedirectToAction("IndexNavbarUnderCategory", "NavbarUnderSettings");
        }
    }
}
