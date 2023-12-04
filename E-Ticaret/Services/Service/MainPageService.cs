using Core.Dtos;
using Core.Model;
using Core.Repositories;
using Core.Services;
using Core.UnitOfWork;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class MainPageService : Service<MainPage>, IMainPageService
    {
        private readonly IService<MainPage> _service;

        public MainPageService(IUnitOfWork unitOfWork, IRepository<MainPage> repository, IService<MainPage> service = null) : base(unitOfWork, repository)
        {
            _service = service;
        }

        public List<MainPage> GetByName(string name)
        {
            return _unitOfWork.MainPageRepository.GetByName(name);
        }

        public async Task RemoveMainPage(string id)
        {
            var data = await _unitOfWork.MainPageRepository.GetByIdAsync(id);

            _unitOfWork.MainPageRepository.DeletePicture(data.image);

            _unitOfWork.MainPageRepository.DeletePicture(data.image2);

            _unitOfWork.MainPageRepository.DeletePicture(data.image3);

            _service.Remove(data);
        }

        public async Task SaveMainPage(MainPageDto request)
        {
           var model = new MainPage();

            model.Id = Guid.NewGuid().ToString();

            model.NavbarProductUnderCategoriesName = request.NavbarProductUnderCategoriesName;

            model.ProductName = request.ProductName;

            model.Stock = request.Stock;

            model.Explanation = request.Explanation;

            model.OldPrices = request.OldPrices;

            model.Prices = request.Prices;


            model.image = _unitOfWork.ProductRepository.CreatePicture(request.image);

            model.image2 = _unitOfWork.ProductRepository.CreatePicture(request.image2);

            model.image3 = _unitOfWork.ProductRepository.CreatePicture(request.image3);

            await _service.AddAsync(model);
        }

        public async Task UpdateMainPage(string id, MainPageDto request)
        {
            var model = await _service.GetByIdAsync(id);

            _unitOfWork.ProductRepository.DeletePicture(model.image);

            _unitOfWork.ProductRepository.DeletePicture(model.image2);

            _unitOfWork.ProductRepository.DeletePicture(model.image3);

            model.image = _unitOfWork.ProductRepository.CreatePicture(request.image);

            model.image2 = _unitOfWork.ProductRepository.CreatePicture(request.image2);

            model.image3 = _unitOfWork.ProductRepository.CreatePicture(request.image3);


            model.NavbarProductUnderCategoriesName = request.NavbarProductUnderCategoriesName;

            model.ProductName = request.ProductName;

            model.Stock = request.Stock;

            model.Explanation = request.Explanation;

            model.OldPrices = request.OldPrices;

            model.Prices = request.Prices;

            _service.Update(model);
        }
    }
}
