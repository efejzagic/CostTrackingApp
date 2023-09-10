using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrelationIdLibrary.Interfaces
{
    public interface ICorrelationIdGenerator
    {
        string Get();

        void Set(string correlationId);
    }
}
