using ShopNetCoreApi.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using ShopNetCoreApi.Models.Entities;

namespace ShopNetCoreApi.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopDbContext _shopDbContext;
        public IUserRepositories UserRepositories { get; private set ; }
        public UnitOfWork(ShopDbContext shopDbContext, IOptions<AppConfig> apOptions)
        {
            _shopDbContext = shopDbContext;
            UserRepositories = new UserRepositories(shopDbContext, apOptions);
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
