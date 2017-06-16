using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Types
{
    public class TransactionGuid : TypedGuid
    {
        public TransactionGuid() : base()
        {
        }

        public TransactionGuid(string id): base(id) { }

        public TransactionGuid(Guid id) : base(id)
        {
        }
    }
}
