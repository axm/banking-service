using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountActor.Tests
{
    [TestFixture]
    public class TransferTests : BaseAccountActorTests
    {
        [Test]
        public void IfNotEnoughBalance_ThrowsInvalidOperationException()
        {
        }
    }
}
