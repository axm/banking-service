using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using BankingIntegrationTests.Attributes;

namespace BankingIntegrationTests.Controllers
{
    [Route("api/[controller]")]
    public class TestsController : Controller
    {
        public async Task RunAll()
        {
            // get all controllers
            // run [BeforeAll]
            // run other methods one by one

            Assembly asm = Assembly.GetExecutingAssembly();

            var methods = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type)) //filter controllers
                .SelectMany(type => type.GetMethods());
            var beforeAll = methods.Where(m => m.IsPublic && m.IsDefined(typeof(BeforeAllAttribute)));

            foreach (var method in beforeAll)
            {
            }
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
