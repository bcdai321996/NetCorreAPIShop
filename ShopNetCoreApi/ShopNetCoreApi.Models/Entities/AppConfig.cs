using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNetCoreApi.Models.Entities
{
    public class AppConfig
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public string Secret { get; set; }

        public string Expries { get; set; }


    }
}
