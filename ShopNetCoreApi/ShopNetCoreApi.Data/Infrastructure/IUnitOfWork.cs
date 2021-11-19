using System;
using System.Collections.Generic;
using System.Text;
using ShopNetCoreApi.Data.Repositories;

namespace ShopNetCoreApi.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        IUserRepositories UserRepositories { get; }
        int CompleteAsync();

        void Dispose();
    }
}
