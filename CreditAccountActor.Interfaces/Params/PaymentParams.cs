using Accounts.Domain;
using Banking.Domain;
using Base.Types;
using Credits.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CreditAccountActor.Interfaces.Params
{
    [DataContract]
    public class PaymentParams
    {
        [DataMember]
        public CreditAccountGuid Id { get; set; }
        [DataMember]
        public AccountGuid FromAccountId { get; set; }
        [DataMember]
        public Money Amount { get; set; }
        [DataMember]
        public DateTimeOffset Timestamp { get; set; }
    }
}
