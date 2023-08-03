using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Domain.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Address{ get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country{ get; set; }
        [Required]
        public int ConstructionSiteId { get; set; }
        public ConstructionSite ConstructionSite { get; set; }
        [Required]
        public double HourlyRate { get; set; }
        [Required]
        public int HoursOfWork { get; set; }
        [Required]
        public double Salary { get; set; }

    }
}
