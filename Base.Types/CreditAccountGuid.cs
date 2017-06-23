using System;

namespace Base.Types
{
    public class CreditAccountGuid : TypedGuid
    {
        public CreditAccountGuid() : this(Guid.NewGuid())
        {
        }

        public CreditAccountGuid(Guid id) : base(id)
        {
        }
    }
}
