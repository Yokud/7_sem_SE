using Microsoft.AspNetCore.Mvc;
using DBLib;
using DBLib.Models;
using DBLib.SysEntities;
using WebAPI.Models;
using System.Diagnostics.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductsRepository productsRepository;
        ICostStoryRepository costs;
        IAvailabilityRepository availabilities;

        public ProductsController(IProductsRepository productsRepository, ICostStoryRepository costStoryRepository, IAvailabilityRepository availabilityRepository)
        {
            this.productsRepository = productsRepository;
            costs = costStoryRepository;
            availabilities = availabilityRepository;
        }


        /// <summary>
        /// Return all products
        /// </summary>
        /// <returns>Collection of products</returns>
        /// <response code="200">Successful operation</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(productsRepository.GetAll().Select(prod => new ProductDTO(prod)));
        }

        /// <summary>
        /// Return product by id
        /// </summary>
        /// <param name="id">id of product</param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Product not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var product = productsRepository.Get(id);

            if (product is null)
                return NotFound();

            return Ok(new ProductDTO(product));
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="product">Product data</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid input</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromQuery] ProductDTO product)
        {
            if (product is null)
                return BadRequest();

            Product prodEntity = product.GetEntity();
            productsRepository.Create(prodEntity);

            return Ok(prodEntity.Id);
        }

        /// <summary>
        /// Add product to shop
        /// </summary>
        /// <param name="shopId">Shop id</param>
        /// <param name="productId">Product id</param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Product or shop not found</response>
        [HttpPost("addProductToShop")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddProductToShop([FromQuery] int shopId, [FromQuery] int productId)
        {
            if (productsRepository.GetAllFromShop(shopId) is null || productsRepository.Get(productId) is null)
                return NotFound();

            Availability availability = new Availability(shopId, productId);
            availabilities.Create(availability);

            return Ok();
        }

        /// <summary>
        /// Delete product from shop
        /// </summary>
        /// <param name="shopId">Shop id</param>
        /// <param name="productId">Product id</param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Product or shop not found</response>
        [HttpDelete("deleteProductFromShop")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProductFromShop([FromQuery] int shopId, [FromQuery] int productId)
        {
            if (productsRepository.GetAllFromShop(shopId) is null || productsRepository.Get(productId) is null)
                return NotFound();

            Availability? availability = availabilities.GetAll().Where(a => a.ShopId == shopId && a.ProductId == productId).FirstOrDefault();

            if (availability is null)
                return NotFound();

            availabilities.Delete(availability);

            return Ok();
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Product not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] ProductDTO product)
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
        /// Delete product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Product not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var product = productsRepository.Get(id);

            if (product is null)
                return NotFound();
                
            productsRepository.Delete(product);

            return Ok();
        }

        /// <summary>
        /// Get products from shop
        /// </summary>
        /// <param name="shopId">Id of shop</param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Product not found</response>
        [HttpGet("findByShop")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult FindByShop([FromQuery] int shopId)
        {
            var prods = productsRepository.GetAllFromShop(shopId);

            if (prods is null)
                return NotFound();

            return Ok(prods.Select(prod => new ProductDTO(prod)));
        }

        /// <summary>
        /// Get product from shop
        /// </summary>
        /// <param name="shopId">Id of shop</param>
        /// <param name="productId">Id of product</param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Product not found</response>
        [HttpGet("{shopId}/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProductFromShop(int shopId, int productId)
        {
            var prods = productsRepository.GetAllFromShop(shopId);

            if (prods is null)
                return NotFound();

            var prod = prods.Where(p => p.Id == productId).FirstOrDefault();

            if (prod is null)
                return NotFound();

            return Ok(new ProductDTO(prod));
        }
    }
}
