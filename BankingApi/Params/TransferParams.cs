using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApi.Params
{
    public class TransferParams
    {
        public Guid From { get; set; }
        public Guid To { get; set; }
        public decimal Amount { get; set; }
    }
}
