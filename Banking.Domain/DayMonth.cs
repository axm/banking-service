using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Domain
{
    public struct DayMonth : IEquatable<DayMonth>, IComparable<DayMonth>
    {
        public int Day { get; private set; }
        public Month Month { get; private set; }

        public DayMonth(int day, Month month)
        {
            Day = day;
            Month = month;
        }

        public int CompareTo(DayMonth other)
        {
            if(Month < other.Month || (Month == other.Month && Day < other.Day))
            {
                return -1;
            }

            if(Month.Equals(other))
            {
                return 0;
            }

            return 1;
        }

        public bool Equals(DayMonth other)
        {
            return Month == other.Month && Day == other.Day;
        }
    }
}
