using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNetCoreApi.Models.Entities
{
    public class AppUserRole
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public AppUser AppUser { get; set; }

        public Role Role { get; set; }

    }
}
