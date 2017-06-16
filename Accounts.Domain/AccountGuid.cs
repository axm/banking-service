using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain
{
    [DataContract]
    public class AccountGuid : Base.Types.TypedGuid
    {
        public AccountGuid() : this(Guid.NewGuid())
        {
        }

        public AccountGuid(Guid id) : base(id)
        {
        }
    }
}
