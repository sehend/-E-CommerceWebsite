using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IBasketRepository : IRepository<Basket>
    {
        public Task<decimal> TotalPrices(string email);

        List<Basket> GetAllWithEmail(string email);

        void DeletePicture(string image);

    }
}
