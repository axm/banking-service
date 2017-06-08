using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTimeOffset GetDateTimeOffset()
        {
            return DateTimeOffset.Now;
        }
    }

    public interface IDateTimeService
    {
        DateTimeOffset GetDateTimeOffset();
    }
}
