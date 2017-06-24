using Base.Providers;
using Base.Types;
using Credits.Interfaces;
using Moq;
using System;

namespace CreditAccountActor.Tests.CreditAccountActor
{
    public class BaseCreditAccountActorTest
    {
        protected Mock<IDateTimeProvider> DateTimeServiceMock;
        protected DateTimeOffset DateTimeOffset = new DateTimeOffset(2017, 06, 01, 12, 00, 00, TimeSpan.Zero);
        protected Mock<ICreditRepository> CreditRepositoryMock;

        public BaseCreditAccountActorTest()
        {
            DateTimeServiceMock = new Mock<IDateTimeProvider>();
            DateTimeServiceMock.Setup(d => d.GetDateTimeOffset()).Returns(DateTimeOffset);

            CreditRepositoryMock = new Mock<ICreditRepository>();
        }
    }
}
