using Core.Model;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class BasketRepository : Repository<Basket>, IBasketRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        public BasketRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<decimal> TotalPrices(string email)
        {
            var model = await _appDbContext.baskets.Where(x => x.CustomerEmail == email).Select(x => x.Prices).ToListAsync();

            decimal price = 0;

            for (int i = 0; i < model.Count; i++)
            {
                price = price + model[i];
            }

            return price;

        }

        public List<Basket> GetAllWithEmail(string email)
        {
            var model = _appDbContext.baskets.Where(x => x.CustomerEmail == email).ToList();

            return model;
        } 
        
        public void RemoveRange(IEnumerable<Basket> baskets)
        {
            _appDbContext.baskets.RemoveRange(baskets);
        }

        public void DeletePicture(string image)
        {
            if (image != null)
            {
                var newPicturePath = Path.Combine("wwwroot", "UserPicture", image);

                FileInfo fi = new FileInfo(newPicturePath);

                System.IO.File.Delete(image);

                fi.Delete();
            }
        }


    }
}
