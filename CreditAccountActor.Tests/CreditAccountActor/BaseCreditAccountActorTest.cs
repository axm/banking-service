using Common.Services;
using Credits.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditAccountActor.Tests.CreditAccountActor
{
    public class BaseCreditAccountActorTest
    {
        protected Mock<IDateTimeService> DateTimeServiceMock;
        protected DateTimeOffset DateTimeOffset = new DateTimeOffset(2017, 06, 01, 12, 00, 00, TimeSpan.Zero);
        protected Mock<ICreditRepository> CreditRepositoryMock;

        public BaseCreditAccountActorTest()
        {
            DateTimeServiceMock = new Mock<IDateTimeService>();
            DateTimeServiceMock.Setup(d => d.GetDateTimeOffset()).Returns(DateTimeOffset);

            CreditRepositoryMock = new Mock<ICreditRepository>();
        }
    }
}
