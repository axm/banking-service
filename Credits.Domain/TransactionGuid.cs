using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credits.Domain
{
    public class TransactionGuid : TypedGuid
    {
        public TransactionGuid() : this(Guid.NewGuid())
        {

        }

        public TransactionGuid(Guid id) : base(id)
        {

        }
    }
}
