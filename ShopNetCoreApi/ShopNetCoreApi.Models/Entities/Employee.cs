using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNetCoreApi.Models.Entities
{
    public class Employee
    {
        public int Id { set; get; }
        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhonePesional { get; set; }

        public string PhoneHome { get; set; }

        public string Linkedin { get; set; }

        public string Facebook { get; set; }

        public string Image { get; set; }

        public DateTime CreateDate { get; set; }

        public List<JobHistory> JobHistorys { get; set; }

        public List<Skill> Skills { get; set; }

        public List<Education> Educations { get; set; }
    }
}
