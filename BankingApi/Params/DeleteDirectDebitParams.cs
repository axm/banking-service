using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApi.Params
{
    public class DeleteDirectDebitParams
    {
        public Guid AccountId { get; set; }
        public Guid DirectDebitId { get; set; }
    }
}
