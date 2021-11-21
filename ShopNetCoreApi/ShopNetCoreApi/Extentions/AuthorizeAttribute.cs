using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopNetCoreApi.Models.Entities;

namespace ShopNetCoreApi.Extentions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public AuthorizeAttribute() : base()
        {

        }
        public  void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (AppUser) context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(new {message = "Unauthorized", code = "401"});
            }
        }
    }
}
