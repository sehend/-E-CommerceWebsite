using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IBasketService : IService<Basket>
    {
        public Task<decimal> TotalPrices(string email);

        List<Basket> GetAllWithEmail(string email);

        void DeleteBasketProducts(List<Basket> baskets);
    }
}
