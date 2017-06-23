using System;

namespace Base.Types
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
