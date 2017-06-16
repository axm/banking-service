using AccountActor.Interfaces;
using Accounts.Domain;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountActor;

namespace AccountActor.Tests
{
    [TestFixture]
    [Author("Alex")]
    public class WithdrawTests : BaseAccountActorTests
    {
        private AccountGuid accountId1 = new AccountGuid(new Guid("bebbb6e2-e0d1-4158-ada1-afc960adff09"));
        private AccountGuid accountId2 = new AccountGuid(new Guid("eaf1752d-5d6b-42e3-a2ad-096164f3e02d"));
        //private AccountActor AccountActor1;
        //private AccountActor AccountActor2;
        private Mock<IAccountRepository> Repository1;
        private Mock<IAccountRepository> Repository2;

        [SetUp]
        public void Setup()
        {
            Repository1 = new Mock<IAccountRepository>();
            Repository1.Setup(r => r.Get(accountId1)).Returns(async () => { return await Task.FromResult((AccountData)null); });

            Repository2 = new Mock<IAccountRepository>();
            Repository2.Setup(r => r.Get(accountId2)).Returns(async () => { return await Task.FromResult((AccountData)null); });
        }

        [Test]
        [Category("InsufficientBalance")]
        public void IfNotEnoughBalance_ReturnsFalse()
        {
            Assert.Fail();
        }

        [Test]
        [Category("EnoughBalance")]
        public void IfEnoughBalance_ReturnsTrue()
        {
            Assert.Fail();
        }
    }
}
