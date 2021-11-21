using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNetCoreApi.Models.Entities
{
    public class AppUser
    {
        public int Id { set; get; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public string PassWord { get; set; }

        public string PasswordSalt { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public string UserName { get; set; }

        public List<Transaction> Transactions { get; set; }

        public List<AppUserRole> AppUserRoles { get; set; }

    }
}
