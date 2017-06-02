using System;
using System.Collections.Generic;
using System.Linq;
using Banking.Domain;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Accounts.Domain;

namespace AccountActor.Interfaces
{
    /// <summary>
    /// This interface defines the methods exposed by an actor.
    /// Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface IAccountActor : IActor
    {
        Task<bool> Withdraw(decimal money);
        Task Deposit(decimal money);
        Task Transfer(Guid to, decimal money);
        Task SetOverdraft(decimal money);
        Task VerifyIntegrity();
        Task SetUpDirectDebit(DirectDebit directDebit);
    }
}
