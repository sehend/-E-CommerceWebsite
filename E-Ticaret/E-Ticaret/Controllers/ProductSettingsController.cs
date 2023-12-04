using Core.Dtos;
using Core.EncapsulationDtos;
using Core.Model;
using Core.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Services.Service;
using X.PagedList;

namespace E_Ticaret.Controllers
{
    public class ProductSettingsController : Controller
    {
        private readonly IProductService _productService;

        private readonly IService<NavbarUnderCategory> _navbarUnderCategoryService;

        public ProductSettingsController(IProductService productService, IService<NavbarUnderCategory> navbarUnderCategoryService)
        {
            _productService = productService;
            _navbarUnderCategoryService = navbarUnderCategoryService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _productService.GetAllAsync();

            return View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            var model = await _productService.GetByIdAsync(id);

            return View(model);
        }

        public IActionResult GetByWithName(string name, int page = 1)
        {
            var products = _productService.GetByNavbarProductUnderCategoriesName(name);

            var model = products.ToPagedList(page, 12);

            return View(model);
        }

        public async Task<IActionResult> DetailsProduct(string id)
        {
            var model = await _productService.GetByIdAsync(id);

            return View(model);
        }

        public async Task<IActionResult> Save()
        {
            var navbarUnderCategory = await _navbarUnderCategoryService.GetAllAsync();

            var model = new ProductAndUnderNavbarEncapsulationModel
            {

                navbarUnderCategories = (List<NavbarUnderCategory>)navbarUnderCategory,




            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductAndUnderNavbarEncapsulationModel request)
        {
            await _productService.SaveProduct(request.ProductDto);

            return RedirectToAction("Index", "ProductSettings");
        }

        public async Task<IActionResult> Update(string id)
        {
            var product = await _productService.GetByIdAsync(id);

            var navbarUnderCategories = await _navbarUnderCategoryService.GetAllAsync();

            var model = new ProductAndUnderNavbarEncapsulationModel
            {

                navbarUnderCategories = (List<NavbarUnderCategory>)navbarUnderCategories,

                Product = product


            };

            return View(model);
        }


        [HttpPost]

        public async Task<IActionResult> Update(string id, ProductAndUnderNavbarEncapsulationModel request)
        {
            request.ProductDto.ProductName = request.Product.ProductName;

            request.ProductDto.NavbarProductUnderCategoriesName = request.Product.NavbarProductUnderCategoriesName;

            request.ProductDto.Stock = request.Product.Stock;

            request.ProductDto.Explanation = request.Product.Explanation;

            request.ProductDto.OldPrices = request.Product.OldPrices;

            request.ProductDto.Prices = request.Product.Prices;

            await _productService.UpdateProduct(id, request.ProductDto);

            return RedirectToAction("Index", "ProductSettings");
        }
        public async Task<IActionResult> Remove(string id)
        {
            await _productService?.RemoveProduct(id);

            return RedirectToAction("Index", "ProductSettings");
        }
    }
}
