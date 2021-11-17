using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNetCoreApi.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopDbContext _shopDbContext;

        public UnitOfWork(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }
        public int CompleteAsync()
        {
            return _shopDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _shopDbContext.Dispose();
        }
    }
}
