using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingPeriodService.Repository
{
    public interface IBillingPeriodRepository
    {
        Task CreateBillingPeriod(DateTime startDate, DateTime endDate);
        Task<BillingPeriod> GetCurrentBillingPeriod();
    }
}
