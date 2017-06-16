using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Types
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
