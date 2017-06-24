using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Providers
{
    public interface IDateTimeProvider
    {
        DateTimeOffset GetDateTimeOffset();
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset GetDateTimeOffset()
        {
            return DateTimeOffset.Now;
        }
    }
}
