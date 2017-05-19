using AccountActor.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountActor.Tests
{
    [TestFixture]
    [Author("Alex")]
    public class DepositTests
    {
        private Mock<IAccountRepository> Repository;

        [SetUp]
        public void Setup()
        {
            Repository = new Mock<IAccountRepository>();
        }


    }
}
