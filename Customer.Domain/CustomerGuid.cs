using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain
{
    public class CustomerGuid : TypedGuid
    {
        public CustomerGuid() : this(Guid.NewGuid())
        {
        }

        public CustomerGuid(Guid id) : base(id)
        {
        }
    }
}
