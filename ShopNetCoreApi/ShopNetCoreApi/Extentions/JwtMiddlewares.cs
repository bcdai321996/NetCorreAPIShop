using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopNetCoreApi.Data.Repositories;
using ShopNetCoreApi.Models.Entities;

namespace ShopNetCoreApi.Extentions
{
    public class JwtMiddlewares
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly AppConfig _appConfig;

        public JwtMiddlewares(RequestDelegate requestDelegate, IOptions<AppConfig> apOptions)
        {
            _requestDelegate = requestDelegate;
            _appConfig = apOptions.Value;
        }

        public async Task Invoke(HttpContext context, IUserRepositories repositories)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null) 
                AttachUserContext(context, repositories, token);
            await _requestDelegate(context);
        }

        private void AttachUserContext(HttpContext context, IUserRepositories repositories, string token)
        {
            try
            {
                var tokenHandle = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appConfig.Secret);
                tokenHandle.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken) validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);
                context.Items["User"] = repositories.GetSingleById(userId);

            }
            catch (Exception ex)
            {
                Debug.Write("Exception"+ ex);
            }
        }
    }
}
