using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ApiEndpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public async Task<string> Teste(string value)
        {

            Thread.Sleep(3000);
            await Task.Delay(0);

            return value;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<string> Get(int id)
        {
            var x = Teste("testando");
            var y = Teste("testando 2");

            var a = await x;
            var b = await y;

            return a + " " + b;
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
