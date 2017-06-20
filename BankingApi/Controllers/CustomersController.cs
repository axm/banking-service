using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankingApi.Params;
using CustomerActor.Interfaces;

namespace BankingApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _repository;

        public CustomersController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task Post(NewCustomerParams param)
        {
            // save new customer
        }

        [HttpPut]
        public async Task Put()
        {
        }
    }
}