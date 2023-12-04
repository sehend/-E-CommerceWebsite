using Core.Dtos;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task RemoveProduct(string id);

        Task SaveProduct(ProductDto request);

        Task UpdateProduct(string id, ProductDto request);

        List<Product> GetByNavbarProductUnderCategoriesName(string name);

        List<Product> GetByName(string name);
    }
}
