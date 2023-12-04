using Core.Model;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;

namespace E_Ticaret.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        private readonly IProductService _productService;

        private readonly IMainPageService _mainPageService;

        private readonly UserManager<AppUser> _userManager;

        public BasketController(IProductService productService, UserManager<AppUser> userManager, IBasketService basketService, IMainPageService mainPageService)
        {
            _productService = productService;
            _userManager = userManager;
            _basketService = basketService;
            _mainPageService = mainPageService;
        }

        public async Task<IActionResult> Index(string id)
        {
            AppUser user = null;

            if (User.Identity.Name == null)
            {
                return RedirectToAction("SignIn", "Home");
            }

            user = await _userManager.FindByNameAsync(User.Identity.Name);

            var data = await _productService.GetByIdAsync(id);

            var data2 = await _mainPageService.GetByIdAsync(id);

            if (data != null)
            {


                var model = new Basket();

                model.Id = Guid.NewGuid().ToString();

                model.CustomerEmail = user.Email;

                model.image = data.image;

                model.ProductName = data.ProductName;

                model.Prices = data.Prices;

                model.Amount = 1;

                model.TotalPrices = await _basketService.TotalPrices(user.Email);

                model.Tarih = DateTime.Now.Date;

                if (model.Amount == 1)
                {

                    var ısProduct = _productService.GetByName(model.ProductName);

                    if (ısProduct == null)
                    {
                        await _basketService.AddAsync(model);
                    }
                }


                var getAll = _basketService.GetAllWithEmail(model.CustomerEmail);

                return View(getAll);

            }

            if (data2 != null)
            {


                var model = new Basket();

                model.Id = Guid.NewGuid().ToString();

                model.CustomerEmail = user.Email;

                model.image = data2.image;

                model.ProductName = data2.ProductName;

                model.Prices = data2.Prices;

                model.Amount = 1;

                model.TotalPrices = await _basketService.TotalPrices(user.Email);

                model.Tarih = DateTime.Now.Date;

                if (model.Amount == 1)
                {
                    var ısMainPageProduct = _mainPageService.GetByName(model.ProductName);

                    if (ısMainPageProduct == null)
                    {
                        await _basketService.AddAsync(model);
                    }

                }


                var getAll = _basketService.GetAllWithEmail(model.CustomerEmail);

                return View(getAll);

            }
            else
            {
                var resualt = _basketService.GetAllWithEmail(user.Email);

                return View(resualt);
            }






        }

        public async Task<IActionResult> AmountPlus(string id)
        {
            var data = await _basketService.GetByIdAsync(id);

            data.Prices = (data.Prices / data.Amount) * (data.Amount + 1);

            data.Amount = data.Amount + 1;

            _basketService.Update(data);

            return RedirectToAction("Index", "Basket");
        }



        public async Task<IActionResult> AmountMinus(string id)
        {

            var data = await _basketService.GetByIdAsync(id);

            if (data.Amount > 1)
            {
                data.Prices = (data.Prices / data.Amount) * (data.Amount - 1);

                data.Amount = data.Amount - 1;

                _basketService.Update(data);
            }


            return RedirectToAction("Index", "Basket");
        }

        public async Task<IActionResult> Remove(string id)
        {
            var data = await _basketService.GetByIdAsync(id);

            _basketService.Remove(data);

            return RedirectToAction("Index", "Basket");
        }
    }
}
