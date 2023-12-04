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
    public class MainPageRepository : Repository<MainPage>, IMainPageRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        private readonly IWebHostEnvironment _webHostEnvironment;

        public MainPageRepository(AppDbContext context, IWebHostEnvironment webHostEnvironment = null) : base(context)
        {
            _webHostEnvironment = webHostEnvironment;
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

        public List<MainPage> GetByName(string name)
        {
            return _appDbContext.mainPages.Where(x => x.ProductName == name).ToList();
        }
    }
}
