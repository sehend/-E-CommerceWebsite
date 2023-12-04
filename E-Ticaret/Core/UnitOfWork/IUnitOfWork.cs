using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        IMainPageRepository  MainPageRepository { get; }

        IProductRepository   ProductRepository { get; }

        IBasketRepository BasketRepository { get; }

        Task CommitAsync();

        void Commit();
    }
}
