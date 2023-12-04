using Core.EncapsulationDtos;
using Core.Model;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.ViewComponents
{
    public class NavbarPartialView : ViewComponent
    {
        private readonly IService<NavbarCategory> _categoryService;

        private readonly IService<NavbarUnderCategory> _underCategoryService;

        public NavbarPartialView(IService<NavbarCategory> categoryService, IService<NavbarUnderCategory> underCategoryService)
        {
            _categoryService = categoryService;
            _underCategoryService = underCategoryService;
        }

        public IViewComponentResult Invoke()
        {
            var category = _categoryService.GetAllAsync().Result;

            var navbarCategories = _underCategoryService.GetAllAsync().Result;

            var model = new HomeIndexEncapsulationModel
            {

                navbarCategories = (List<NavbarCategory>)category,

                navbarUnderCategories = (List<NavbarUnderCategory>)navbarCategories


            };


            return View(model);
        }
    }
}
