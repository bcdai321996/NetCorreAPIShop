using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNetCoreApi.Models.Entities
{
    public class Skill
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public int Experience { set; get; }

        public int IsDelete { set; get; }

        public Employee Employee { get; set; }

    }
}
