using System;
using System.Runtime.Serialization;

namespace Base.Types
{
    [DataContract]
    public class AccountGuid : TypedGuid
    {
        public AccountGuid() : this(Guid.NewGuid())
        {
        }

        public AccountGuid(Guid id) : base(id)
        {
        }
    }
}
