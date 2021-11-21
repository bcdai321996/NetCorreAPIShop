using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNetCoreApi.Models.ViewModel
{
    public class AuthenticateRequest
    {
       public string UserName { get; set; }
       public string PassWord { get; set; }
       
    }
}
