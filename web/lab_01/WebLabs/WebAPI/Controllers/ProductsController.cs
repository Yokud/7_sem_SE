using Microsoft.AspNetCore.Mvc;
using DBLib;
using DBLib.Models;
using DBLib.SysEntities;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductsRepository productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(productsRepository.GetAll().Select(prod => new ProductDTO(prod)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var product = productsRepository.Get(id);

            if (product is null)
                return NotFound();

            return Ok(new ProductDTO(product));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        [HttpPost]
        public ActionResult Post([FromBody] ProductDTO product)
        {
            if (product is null)
                return BadRequest();

            Product prodEntity = product.GetEntity();
            productsRepository.Create(prodEntity);

            return Ok(prodEntity.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProductDTO product)
        {
            Product prod = productsRepository.Get(id);

            if (prod is null)
                return NotFound();

            prod.Name = product.Name;
            prod.ProductType = product.ProductType;

            productsRepository.Update(prod);

            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var product = productsRepository.Get(id);

            if (product is null)
                return NotFound();
                
            productsRepository.Delete(product);

            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>
        [HttpGet("findByShop")]
        public ActionResult FindByShop([FromQuery] ShopDTO shop)
        {
            var prods = productsRepository.GetAllFromShop(shop.GetEntity());

            if (prods is null)
                return NotFound();

            return Ok(prods.Select(prod => new ProductDTO(prod)));
        }

        [HttpGet("{shopId}/{productId}")]
        public ActionResult GetProductFromShop(int shopId, int ProductId)
        {
            return Ok();
        }
    }
}
