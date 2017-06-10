using Banking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credits.Domain
{
    public class CreditAccount
    {
        public CreditAccountGuid Id { get; private set; }
        public Money Limit { get; private set; }
        public decimal Interest { get; private set; }
        public Money Usage { get; private set; }
        public Money AvailableFunds { get; private set; }

        public CreditAccount(CreditAccountGuid id, Money limit, decimal interest, Money usage, Money availableFunds)
        {
            Id = id;
            Limit = limit;
            Interest = interest;
            Usage = usage;
            AvailableFunds = availableFunds;
        }
    }
}
