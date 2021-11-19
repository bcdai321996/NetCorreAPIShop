using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopNetCoreApi.Extentions
{
    public class Responses
    {
        public string Status { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }

        public Responses(string status, string code, string message)
        {
            Status = status;
            Code = code;
            Message = message;
        }
    }
}
