using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNetCoreApi.Models.Entities
{
    public class JobHistory
    {
        public int Id { set; get; }
        public string Name { set; get; }

        public DateTime StartDate { set; get; }

        public DateTime EndDate { set; get; }

        public DateTime CompanyName { set; get; }

        public string JobInformation { set; get; }

        public Employee Employee { get; set; }

    }
}
