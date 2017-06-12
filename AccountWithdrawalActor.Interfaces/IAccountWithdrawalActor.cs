using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Banking.Domain;
using Accounts.Domain;

namespace AccountWithdrawalActor.Interfaces
{
    /// <summary>
    /// This interface defines the methods exposed by an actor.
    /// Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface IAccountWithdrawalActor : IActor
    {
        Task Withdraw(AccountGuid accountId, Money money);
    }
}
