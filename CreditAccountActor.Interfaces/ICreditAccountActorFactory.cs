using Credits.Domain;
using Microsoft.ServiceFabric.Actors.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditAccountActor.Interfaces
{
    public interface ICreditAccountActorFactory
    {
        ICreditAccountActor Create(CreditAccountGuid id);
    }


    public class CreditAccountActorFactory : ICreditAccountActorFactory
    {
        public ICreditAccountActor Create(CreditAccountGuid id)
        {
            return ActorProxy.Create<ICreditAccountActor>(new Microsoft.ServiceFabric.Actors.ActorId(id), new Uri(""));
        }
    }
}
