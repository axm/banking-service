using Banking.Domain;
using System;
using System.Runtime.Serialization;

namespace Accounts.Domain
{
    [DataContract]
    public class DirectDebit
    {
        [DataMember]
        public DirectDebitGuid Id { get; set; }
        [DataMember]
        public Money Amount { get; set; }
        [DataMember]
        public AccountGuid FromAccountId { get; set; }
        [DataMember]
        public AccountGuid ToAccountId { get; set; }
        [DataMember]
        public DateTimeOffset StartDate { get; set; }
        [DataMember]
        public DirectDebitFrequency Frequency { get; set; }
    }
}