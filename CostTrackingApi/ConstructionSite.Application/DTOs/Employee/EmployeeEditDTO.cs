using ConstructionSite.Application.DTOs.ConstructionSite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Application.DTOs.Employee
{
    public class EmployeeEditDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int ConstructionSiteId { get; set; }
        public double HourlyRate { get; set; }
        public int HoursOfWork { get; set; }
        public double Salary { get; set; }
    }
}
