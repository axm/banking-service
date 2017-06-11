using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain
{
    public class AccountInfo
    {
        public AccountGuid Id { get; private set; }

        public AccountInfo(AccountGuid id)
        {
            Id = id;
        }
    }
}
