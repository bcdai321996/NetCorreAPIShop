using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopNetCoreApi.Extentions
{
    public class AuthContextService
    {
        private static IHttpContextAccessor _context;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;
        }
        public static HttpContext Current => _context.HttpContext;
    }
}
