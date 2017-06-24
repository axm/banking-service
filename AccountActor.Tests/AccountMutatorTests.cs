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
        private AccountGuid AccountId = new AccountGuid(new Guid("7c4ff7d4-cec4-4f3b-843b-e304ebf36f6e"));
        private SortCode SortCode = new SortCode("10-10-10");
        private AccountData AccountData;
        private AccountMutator Mutator;
        private TransactionGuid DepositId = new TransactionGuid("b8dd8812-58a5-4395-aedb-df323d21ab9e");
        private Transaction Deposit;
        private Money Balance = new Money(1000);
        private Money DepositAmount = new Money(100);

        [SetUp]
        public void Setup()
        {
            var time = new DateTimeOffset(2017, 01, 01, 0, 0, 0, TimeSpan.FromHours(1));
            AccountData = new AccountData(AccountId, SortCode, Money.Zero, Balance);
            Mutator = new AccountMutator(AccountData);
            Deposit = new Transaction(DepositId, null, AccountId, time, Balance, DepositAmount);
        }

        [Test]
        public void ApplyTransaction_NullArgument_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Mutator.ApplyTransaction(null));
        }

        [Test]
        public void ApplyTransactions_AddNewTransactionToAccount()
        {
            var initialTransactionCount = AccountData.Transactions.Count;

            Mutator.ApplyTransaction(Deposit);

            Assert.AreEqual(initialTransactionCount + 1, AccountData.Transactions.Count);
        }

        [Test]
        public void ApplyTransaction_UpdatesAccountBalance()
        {
            Mutator.ApplyTransaction(Deposit);

            Assert.AreEqual(new Money(1100), AccountData.Balance);
        }
    }
}
