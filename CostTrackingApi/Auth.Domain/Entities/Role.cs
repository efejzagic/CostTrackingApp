using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Entities
{
    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Composite { get; set; }

        public bool ClientRole { get; set; }
        public string ContainerId { get;set; }
    }
}
