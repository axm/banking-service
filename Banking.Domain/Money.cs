using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Domain
{
    public class Money
    {
        public decimal Amount { get; private set; }

        public Money()
        {
        }

        public Money(decimal amount)
        {
            if(amount < 0)
            {
                throw new ArgumentOutOfRangeException($"Amount must be 0 or greater. Amount: {amount}");
            }

            Amount = amount;
        }

        public static Money operator +(Money par1, Money par2)
        {
            return par1 + par2;
        }

        public static Money operator -(Money par1, Money par2)
        {
            if(par1 < par2)
            {
                throw new InvalidOperationException($"Cannot subtract a bigger value from a smaller value. Param 1: {par1}, Param 2: {par2}");
            }

            return (par1 - par2);
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
