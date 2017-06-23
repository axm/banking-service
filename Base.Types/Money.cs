using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Base.Types
{
    [DataContract]
    [DebuggerDisplay("{Amount}")]
    public struct Money
    {
        public static Money Zero = new Money(0);
        [DataMember]
        public readonly decimal Amount;

        public Money(decimal amount)
        {
            if(amount < 0)
            {
                throw new ArgumentOutOfRangeException($"Amount must be 0 or greater. Amount: {amount}");
            }

            Amount = amount;
        }

        public static implicit operator decimal(Money money) 
        {
            return money.Amount;
        }

        public static implicit operator Money(decimal amount)
        {
            return new Money(amount);
        }
    }
}
