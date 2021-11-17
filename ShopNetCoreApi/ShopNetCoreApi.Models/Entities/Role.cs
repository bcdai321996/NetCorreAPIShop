using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNetCoreApi.Models.Entities
{
    public class Role
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public string Description { set; get; }
        public string Status { set; get; }

        public List<AppUserRole> AppUserRoles { get; set; }

        public List<Permission> Permissions { get; set; }

    }
}
