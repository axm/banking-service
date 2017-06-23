using Base.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain
{
    public class AccountSettings
    {
        public AccountGuid AccountId { get; set; }
        public decimal Overdraft { get; set; }
    }
}
