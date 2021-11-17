using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNetCoreApi.Models.Entities
{
    public class Permission
    {
        public int RoleId { get; set;  }

        public int ActionId { get; set; }
        public int FunctionId { get; set; }

        public Role Role { get; set; }

        public Action Action { get; set; }

        public Function Function { get; set; }
    }
}
