using Microsoft.AspNetCore.Mvc;
using DBLib;
using DBLib.Models;
using DBLib.SysEntities;
using WebAPI.Models;
using System.Text.Json;
using System.Diagnostics;

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

        /// <summary>
        /// Get all shops
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(shopsRepository.GetAll().Select(shop => new ShopDTO(shop)));
        }

        /// <summary>
        /// Get shop by id
        /// </summary>
        /// <param name="id">ID of shop to return</param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Shop not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shop))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(int id)
        {
            var res = shopsRepository.Get(id);

            if (res is null)
                return NotFound();

            return Ok(new ShopDTO(res));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public ActionResult Post([FromBody] string value)
        {
            ShopDTO? shopDTO = JsonSerializer.Deserialize<ShopDTO>(value);

            if (shopDTO is null)
                return BadRequest(value);

            Shop shop = shopDTO.GetEntity();
            shopsRepository.Create(shop);

            return Ok(shop.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            return Ok();
        }

        /// <summary>
        /// Delete shop by id
        /// </summary>
        /// <param name="id">ID of shop to return</param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Shop not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shop))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            var deletedShop = shopsRepository.Get(id);

            if (deletedShop is null)
                return NotFound();
              
            shopsRepository.Delete(deletedShop);
            shopsRepository.Save();

            return Ok();
        }
    }
}
