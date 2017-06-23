using System;

namespace BankingApi.Params
{
    public class DepositParams
    {
        public Guid AccountId { get; set; }
        public decimal Money { get; set; }
    }
}
