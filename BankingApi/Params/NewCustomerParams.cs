using System;

namespace BankingApi.Params
{
    public class NewCustomerParams
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}