using Core.Dtos;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IMainPageService : IService<MainPage>
    {
        Task SaveMainPage(MainPageDto request);

        Task UpdateMainPage(string id, MainPageDto request);

        Task RemoveMainPage(string id);

        List<MainPage> GetByName(string name);
    }
}
