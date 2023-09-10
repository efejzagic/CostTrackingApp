using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrelationIdLibrary.Interfaces
{
    public class CorrelationIdGenerator : ICorrelationIdGenerator
    {
        private string _correlationId = Guid.NewGuid().ToString();

        public string Get() => _correlationId;
        

        public void Set(string correlationId) => _correlationId = correlationId;


    }
}
