using Core.EncapsulationDtos;
using Core.Model;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Services.Service;

namespace E_Ticaret.Controllers
{
    public class NavbarSettingsController : Controller
    {
        private readonly IService<NavbarCategory> _categoryService;



        public NavbarSettingsController(IService<NavbarCategory> categoryService)
        {
            _categoryService = categoryService;

        }

        public async Task<IActionResult> IndexCategory()
        {
            return View(await _categoryService.GetAllAsync());
        }

        public IActionResult SaveNavbarCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveNavbarCategory(NavbarCategory navbarCategory)
        {
            navbarCategory.Id = Guid.NewGuid().ToString();

            await _categoryService.AddAsync(navbarCategory);

            return RedirectToAction("IndexCategory", "NavbarSettings");
        }

        public async Task<IActionResult> UpdateNavbarCategory(string id)
        {
            var model = await _categoryService.GetByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateNavbarCategory(NavbarCategory navbarCategory)
        {
            _categoryService.Update(navbarCategory);

            return RedirectToAction("IndexCategory", "NavbarSettings");
        }

        public async Task<IActionResult> RemoveNavbarCategory(string id)
        {
            var model = await _categoryService.GetByIdAsync(id);

            _categoryService?.Remove(model);

            return RedirectToAction("IndexCategory", "NavbarSettings");
        }



    }
}
