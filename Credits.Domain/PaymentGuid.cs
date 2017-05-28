using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credits.Domain
{
    public class PaymentGuid : TypedGuid
    {
        public PaymentGuid() : this(Guid.NewGuid())
        {
        }

        public PaymentGuid(Guid id) : base(id)
        {
        }
    }
}
