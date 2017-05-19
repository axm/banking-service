using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApi.Params
{
    public class WithdrawParams
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
