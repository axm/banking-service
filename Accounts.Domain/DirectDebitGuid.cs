using Base.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain
{
    public class DirectDebitGuid : TypedGuid
    {
        public DirectDebitGuid() : this(Guid.NewGuid())
        {
        }

        public DirectDebitGuid(Guid id) : base(id)
        {
        }
    }
}
