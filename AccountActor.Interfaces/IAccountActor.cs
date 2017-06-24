using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Accounts.Domain;
using Base.Types;
using Accounts.Entities;

namespace AccountActor.Interfaces
{
    /// <summary>
    /// This interface defines the methods exposed by an actor.
    /// Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface IAccountActor : IActor
    {
        Task MakeTransaction(AccountGuid outputAccountId, DateTimeOffset timestamp, Money amount);
        Task SetOverdraft(Money money);
        Task VerifyIntegrity();
        Task PostDirectDebit(Money amount, AccountGuid toAccountId, DateTimeOffset startTime, DirectDebitFrequency frequency);
        Task DeleteDirectDebit(DirectDebitGuid directDebitId);
        Task<AccountInfo> GetAccountInfo(MonthYear monthYear);
        Task ApplyInterest();
    }
}
