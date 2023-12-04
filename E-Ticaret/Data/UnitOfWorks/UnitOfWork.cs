using Core.Repositories;
using Core.UnitOfWork;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private MainPageRepository _mainPageRepository;

        private ProductRepository _productRepository;

        private BasketRepository _basketRepository { get; set; }

        public IMainPageRepository MainPageRepository => _mainPageRepository = _mainPageRepository ?? new MainPageRepository(_context);

        public IProductRepository ProductRepository => _productRepository = _productRepository ?? new ProductRepository(_context);

        public IBasketRepository BasketRepository => _basketRepository = _basketRepository ?? new BasketRepository(_context);

        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
