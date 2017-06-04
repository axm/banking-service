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
        private readonly IntegrationTestData Data = new IntegrationTestData();

        [HttpGet("RunAll")]
        public async Task RunAll()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var controllers = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type) && type.IsDefined(typeof(IntegrationTestControllerAttribute)));

            foreach (var controller in controllers)
            {
                var ctrl = Data.Container.GetInstance(controller); //Activator.CreateInstance(controller);

                var beforeAll = controller.GetMethods().Where(m => m.IsPublic && m.IsDefined(typeof(BeforeAllAttribute)));
                foreach (var method in beforeAll)
                {
                    method.Invoke(ctrl, null);
                }

                var testMethods = controller.GetMethods().Where(m => m.IsPublic && m.IsDefined(typeof(IntegrationTestAttribute)));
                foreach (var method in testMethods)
                {
                    method.Invoke(ctrl, null);
                }
            }
        }
    }
}
