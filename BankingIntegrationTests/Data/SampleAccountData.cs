using Accounts.Domain;
using Banking.Domain;
using Credits.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingIntegrationTests.Data
{
    public class SampleAccountData
    {
        public static AccountData Account1 = new AccountData(new AccountGuid(new Guid("1FF8A281-026B-4C58-AA67-8E3657DD21B4")),
            new SortCode("90-60-90"),
            Money.Zero,
            new Money(12000), null);
        public static AccountData Account2 = new AccountData(new AccountGuid(new Guid("565eff3a-dae6-44a4-b8cb-cef2cd0d4d31")),
            new SortCode("90-60-90"),
            Money.Zero,
            new Money(1500), null);
        public static AccountData Account3 = new AccountData(new AccountGuid(new Guid("4bc30910-d12a-4c8e-bba9-e82597bddbf4")),
            new SortCode("90-60-90"),
            Money.Zero,
            new Money(7000), null);

        public static Money Deposit1 = Money.Zero;
        public static Money Deposit2 = new Money(250);

        public static Money Withdrawal1 = Money.Zero;
        public static Money Withdrawal2 = new Money(300);
        public static Money Withdrawal3 = new Money(15000);
    }
}
