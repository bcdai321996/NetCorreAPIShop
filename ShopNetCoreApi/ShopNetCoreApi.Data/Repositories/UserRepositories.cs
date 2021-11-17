using ShopNetCoreApi.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using ShopNetCoreApi.Models.Entities;

namespace ShopNetCoreApi.Data.Repositories
{
    public class UserRepositories : RepositoryBase<AppUser>, IUserRepositories
    {
        private readonly ShopDbContext _dbContext;
        public AppConfig AppConfig { get; private set; }
        public UserRepositories(ShopDbContext dbContext, IOptions<AppConfig> apOptions) : base(dbContext)
        {
            _dbContext = dbContext;
            AppConfig = apOptions.Value;
        }
    }
}
