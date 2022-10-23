using Microsoft.AspNetCore.Mvc;
using DBLib;
using DBLib.Models;
using DBLib.SysEntities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/shops")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        IShopsRepository shopsRepository;

        public ShopsController(IShopsRepository shops)
        {
            shopsRepository = shops;
        }

        // GET: api/<ShopsController>
        [HttpGet]
        public IEnumerable<Shop> Get()
        {
            return shopsRepository.GetAll();
        }

        // GET api/<ShopsController>/5
        [HttpGet("{id}")]
        public Shop Get(int id)
        {
            var res = shopsRepository.Get(id);

            if (res is null)
                NotFound();

            return res;
        }

        // POST api/<ShopsController>
        [HttpPost]
        public void Post([FromBody] string shopJSON)
        {
            //shops.Create(shop);
        }

        // PUT api/<ShopsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string shopJSON)
        {
        }

        // DELETE api/<ShopsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var deletedShop = shopsRepository.Get(id);

            if (deletedShop is null)
            {
                NotFound();
                return;
            }
              
            shopsRepository.Delete(deletedShop);
            shopsRepository.Save();
        }
    }
}
