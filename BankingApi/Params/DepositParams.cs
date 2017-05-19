using Accounts.Domain;
using Banking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApi.Params
{
    public class DepositParams
    {
        public Guid AccountId { get; set; }
        public decimal Money { get; set; }
    }
}
