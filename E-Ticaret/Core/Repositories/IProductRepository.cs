using Core.Dtos;
using Core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetProductCategoryNameAsync(string productCategoryName);

        void DeletePicture(string image);

        string CreatePicture(IFormFile image);

        List<Product> GetByNavbarProductUnderCategoriesName(string name);

        List<Product> GetByName(string name);
    }
}
