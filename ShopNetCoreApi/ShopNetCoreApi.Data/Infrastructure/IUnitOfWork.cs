using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNetCoreApi.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        int CompleteAsync();

        void Dispose();
    }
}
