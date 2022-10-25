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
        /// Create new shop
        /// </summary>
        /// <param name="shop">New shop object</param>
        /// <response code="200">Successful operation</response>
        /// <response code="405">Invalid input</response>
        [HttpPost]
        public ActionResult Post([FromBody] ShopDTO shop)
        {
            if (shop is null)
                return BadRequest();

            Shop shopEntity = shop.GetEntity();
            shopsRepository.Create(shopEntity);

            return Ok(shopEntity.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shop"></param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Not found</response>
        /// <response code="405">Invalid input</response>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ShopDTO shop)
        {
            var putShop = shopsRepository.Get(id);

            if (putShop is null)
                return NotFound();

            putShop.Name = shop.Name;
            putShop.Description = shop.Description;

            shopsRepository.Update(putShop);

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
