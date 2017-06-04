using Accounts.Domain;
using CreditAccountActor.Interfaces;
using CreditPaymentsActor.Interfaces;
using CreditTransactionsActor.Interfaces;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingIntegrationTests.Controllers
{
    internal class Account
    {
    }

    internal class IntegrationTestData
    {
        public static IntegrationTestData Instance = new IntegrationTestData();

        public readonly AccountGuid AccountId1 = new AccountGuid(new Guid("574b1f71-ba84-4b94-9873-b7b47272b065"));
        public readonly AccountGuid AccountId2 = new AccountGuid(new Guid("047206e9-9ca8-4950-8b4a-1432de0f3c5a"));
        public readonly Container Container = new Container();

        public IntegrationTestData()
        {
            Container.Register<ICreditAccountActorFactory, CreditAccountActorFactory>();
            Container.Register<ICreditTransactionsActorFactory, CreditTransactionsActorFactory>();
            Container.Register<ICreditPaymentsActorFactory, CreditPaymentsActorFactory>();
            Container.Register<CreditsController>();
            Container.Register<AccountsController>();
        }
    }
}
