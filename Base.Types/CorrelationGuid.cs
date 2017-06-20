using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Types
{
    public class CorrelationGuid : TypedGuid
    {
        public CorrelationGuid() : base(new Guid())
        {
        }

        public CorrelationGuid(string id): base(id) { }

        public CorrelationGuid(Guid id): base(id) { }
    }
}
