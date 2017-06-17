using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Types
{
    public interface IDateTimeService
    {
        DateTimeOffset GetDateTimeOffset();
    }

    public class DateTimeService : IDateTimeService
    {
        public DateTimeOffset GetDateTimeOffset()
        {
            return DateTimeOffset.Now;
        }
    }
}
