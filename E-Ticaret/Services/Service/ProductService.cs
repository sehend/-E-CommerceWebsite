using Azure.Core;
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
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IService<Product> _service;

        public ProductService(IUnitOfWork unitOfWork, IRepository<Product> repository, IService<Product> service = null) : base(unitOfWork, repository)
        {
            _service = service;
        }

        public List<Product> GetByName(string name)
        {
            return _unitOfWork.ProductRepository.GetByName(name);
        }

        public List<Product> GetByNavbarProductUnderCategoriesName(string name)
        {
            var model = _unitOfWork.ProductRepository.GetByNavbarProductUnderCategoriesName(name);

            return model;
        }

        public async Task RemoveProduct(string id)
        {
            var products = await _service.GetByIdAsync(id);

            _unitOfWork.ProductRepository.DeletePicture(products.image);

            _unitOfWork.ProductRepository.DeletePicture(products.image2);

            _unitOfWork.ProductRepository.DeletePicture(products.image3);

            _service.Remove(products);

        }

        public async Task SaveProduct(ProductDto request)
        {
            var model = new Product();

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

        public async Task UpdateProduct(string id, ProductDto request)
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
