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
        Task<bool> Withdraw(Money money);
        Task Deposit(Money money);
        Task Transfer(AccountGuid to, Money money);
        Task ApplyTransaction(AccountGuid inputAccountId, AccountGuid outputAccountId, DateTimeOffset timestamp, Money amount);
        Task SetOverdraft(Money money);
        Task VerifyIntegrity();
        Task PostDirectDebit(Money amount, AccountGuid toAccountId, DateTimeOffset startTime, DirectDebitFrequency frequency);
        Task DeleteDirectDebit(DirectDebitGuid directDebitId);
        Task<AccountInfo> GetAccountInfo(MonthYear monthYear);
        Task ApplyInterest();
    }
}
