using ConstructionSite.Application.DTOs.Employee;
using ConstructionSite.Application.Parameters;
using ConstructionSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Application.DTOs.ConstructionSite
{
    public  class ConstructionSiteDTO : RequestParameter
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public List<EmployeeConstructionSiteDTO> Employees { get; set; }


    }
}
