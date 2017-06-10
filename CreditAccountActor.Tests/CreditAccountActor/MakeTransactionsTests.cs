using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CreditAccountActor.Tests.CreditAccountActor
{
    public class MakeTransactionsTests : BaseCreditAccountActorTest
    {
        [Fact]
        public async Task WhenRunForTheFirstTime_LoadsFromRepository()
        {
        }

        [Fact]
        public async Task When_Not_RunForTheFirstTime_Does_Not_LoadFromRepository()
        {
        }


    }
}
