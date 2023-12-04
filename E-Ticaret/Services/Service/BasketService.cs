using Core.Model;
using Core.Repositories;
using Core.Services;
using Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class BasketService : Service<Basket>, IBasketService
    {
        private readonly IService<Basket> _service;

        public BasketService(IUnitOfWork unitOfWork, IRepository<Basket> repository, IService<Basket> service) : base(unitOfWork, repository)
        {
            _service = service;
        }

        public List<Basket> GetAllWithEmail(string email)
        {
            return _unitOfWork.BasketRepository.GetAllWithEmail(email);
        }

        public Task<decimal> TotalPrices(string email)
        {
            return _unitOfWork.BasketRepository.TotalPrices(email);
        }  
        
        public void DeleteBasketProducts(List<Basket> baskets)
        {
            foreach (var item in baskets)
            {
                _service.Remove(item);
            }
        }
    }
}
