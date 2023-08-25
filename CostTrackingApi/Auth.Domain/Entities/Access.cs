using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Entities
{
    public  class Access
    {
        public bool manageGroupMembership { get; set; }
        public bool view { get; set; }
        public bool mapRoles { get; set; }
        public bool impersonate { get; set; }
        public bool manage { get; set; }
    }
}
