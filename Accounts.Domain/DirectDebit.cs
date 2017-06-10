using Banking.Domain;
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
    }
}