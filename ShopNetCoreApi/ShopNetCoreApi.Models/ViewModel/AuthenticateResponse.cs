using System;
using System.Collections.Generic;
using System.Text;
using ShopNetCoreApi.Models.Entities;

namespace ShopNetCoreApi.Models.ViewModel
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public AuthenticateResponse(AppUser user, string token)
        {
            Id = user.Id;
            Name = user.UserName;
            Token = token;
        }
    }
}
