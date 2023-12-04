using Core.Dtos;
using Core.Model;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Services.Service;

namespace E_Ticaret.Controllers
{
    public class MainPageSettingsController : Controller
    {
        private readonly IMainPageService  _mainPageService;

        public MainPageSettingsController(IMainPageService  mainPageService)
        {
            _mainPageService = mainPageService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _mainPageService.GetAllAsync());
        }


        public async Task<IActionResult> DetailsMainPage(string id)
        {
            var model = await _mainPageService.GetByIdAsync(id);

            return View(model);
        }

        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(MainPageDto request)
        {
            await _mainPageService.SaveMainPage(request);

            return RedirectToAction("Index", "MainPageSettings");
        }

        public async Task<IActionResult> Update(string id)
        {
            return View(await _mainPageService.GetByIdAsync(id));
        }


        [HttpPost]

        public async Task<IActionResult> Update(string id, MainPageDto request)
        {
            await _mainPageService.UpdateMainPage(id, request);

            return RedirectToAction("Index", "MainPageSettings");
        }
        public async Task<IActionResult> Remove(string id)
        {
            await _mainPageService?.RemoveMainPage(id);

            return RedirectToAction("Index", "MainPageSettings");
        }
    }
}
