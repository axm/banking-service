using AccountActor.Interfaces;
using Base.Providers;
using Base.Types;
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
    public class AccountActorTests
    {
        private Mock<IAccountRepository> MockAccountRepository;
        private Mock<IDateTimeProvider> MockDateTimeService;
        private Mock<IServiceBusProvider> MockServiceBusProvider;
        private Mock<IElasticSearchProvider> MockElasticSearchProvider;
    }
}
