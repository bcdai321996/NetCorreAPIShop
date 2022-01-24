using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNetCoreApi.Models.Entities
{
    public class Education
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string UniversityName { set; get; }
        public string Degree { set; get; }

        public DateTime StartDate { set; get; }

        public DateTime EndDate { set; get; }

        public string EducationInformation { set; get; }

        public Employee Employee { get; set; }

    }
}
