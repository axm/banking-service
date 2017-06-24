using System;
using System.Runtime.Serialization;

namespace Base.Types
{
    [DataContract]
    public class AccountGuid : TypedGuid
    {
        public AccountGuid() : base(Guid.NewGuid())
        {
        }

        public AccountGuid(string id) : base(id)
        {
        }

        public AccountGuid(Guid id) : base(id)
        {
        }
    }
}
