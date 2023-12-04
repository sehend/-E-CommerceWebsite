using Core.Dtos;
using Core.Model;
using Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductRepository(AppDbContext context, IWebHostEnvironment webHostEnvironment) : base(context)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public List<Product> GetProductCategoryNameAsync(string NavbarProductUnderExplanation)
        {

            return _appDbContext.products.Where(x => x.NavbarProductUnderCategoriesName == NavbarProductUnderExplanation).ToList();
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


        public string CreatePicture(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {

                var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(image.FileName)}";

                string path = Path.Combine("wwwroot" + "/UserPicture", randomFileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                return randomFileName;

            }

            return null;
        }

        public List<Product> GetByNavbarProductUnderCategoriesName(string name)
        {
            var model = _appDbContext.products.Where(x=>x.NavbarProductUnderCategoriesName == name).ToList();

            return model;
        }

        public List<Product> GetByName(string name)
        {
            var model = _appDbContext.products.Where(x => x.ProductName == name).ToList();

            return model;
        }
    }
}
