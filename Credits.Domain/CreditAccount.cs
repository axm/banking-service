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
        public CreditAccountGuid Id { get; set; }
        public Money Limit { get; set; }
        public decimal Interest { get; set; }
        public Money Usage { get; set; }

        public CreditAccount(CreditAccountGuid id, Money limit, decimal interest, Money usage)
        {
            Id = id;
            Limit = limit;
            Interest = interest;
            Usage = usage;
        }
    }
}
