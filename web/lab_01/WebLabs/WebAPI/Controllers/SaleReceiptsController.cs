using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleReceiptsController : ControllerBase
    {
        // GET: api/<SaleReceiptsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SaleReceiptsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SaleReceiptsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SaleReceiptsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SaleReceiptsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
