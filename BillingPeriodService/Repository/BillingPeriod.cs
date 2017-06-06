using System;

namespace BillingPeriodService.Repository
{
    public class BillingPeriod
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}