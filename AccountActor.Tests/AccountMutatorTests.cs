using Accounts.Domain;
using Accounts.Entities;
using Base.Types;
using NUnit.Framework;
using System;

namespace AccountActor.Tests
{
    [TestFixture]
    public class AccountMutatorTests
    {
        private AccountGuid AccountId;
        private SortCode SortCode;
        private AccountData AccountData;
        private AccountMutator Mutator;

        [SetUp]
        public void Setup()
        {
            AccountId = new AccountGuid(new Guid("7c4ff7d4-cec4-4f3b-843b-e304ebf36f6e"));
            SortCode = new SortCode("10-10-10");
            AccountData = new AccountData(AccountId, SortCode);
            Mutator = new AccountMutator(AccountData);
        }

        [Test]
        public void ApplyTransaction_NullArgument_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Mutator.ApplyTransaction(null));
        }

        [Test]
        public void ApplyTransactions_AddNewTransactionToAccount()
        {
        }

        [Test]
        public void ApplyTransaction_UpdatesAccountBalance()
        {
        }
    }
}
