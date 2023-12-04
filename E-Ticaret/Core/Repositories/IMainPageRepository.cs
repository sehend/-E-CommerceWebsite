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
    public interface IMainPageRepository : IRepository<MainPage>
    {
        void DeletePicture(string image);

        string CreatePicture(IFormFile image);

        List<MainPage> GetByName(string name);
    }
}
