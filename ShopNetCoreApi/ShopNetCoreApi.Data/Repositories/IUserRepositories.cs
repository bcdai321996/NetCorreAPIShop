using System;
using System.Collections.Generic;
using System.Text;
using ShopNetCoreApi.Data.Infrastructure;
using ShopNetCoreApi.Models.Entities;
using ShopNetCoreApi.Models.ViewModel;

namespace ShopNetCoreApi.Data.Repositories
{
    public interface IUserRepositories : IRepository<AppUser>
    {
        AuthenticateResponse Authenticate(string userName, string passWord);


    }
}
