using Base.Types;
using System;

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
