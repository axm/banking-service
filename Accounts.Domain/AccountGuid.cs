using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain
{
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
