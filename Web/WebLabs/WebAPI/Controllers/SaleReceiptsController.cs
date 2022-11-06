using DBLib.DB;
using DBLib.Models;
using DBLib.SysEntities;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/salereceipts")]
    [ApiController]
    public class SaleReceiptsController : ControllerBase
    {
        ISaleReceiptsRepository saleReceipts;
        ISaleReceiptPositionsRepository saleReceiptPositions;

        public SaleReceiptsController(ISaleReceiptsRepository saleReceipts, ISaleReceiptPositionsRepository saleReceiptPositions)
        {
            this.saleReceipts = saleReceipts;
            this.saleReceiptPositions = saleReceiptPositions;
        }

        /// <summary>
        /// Get all sale receipts
        /// </summary>
        /// <returns>Sale receipts</returns>
        /// <response code="200">Successful operation</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(saleReceipts.GetAll().Select(sr => new SaleReceiptDTO(sr)));
        }

        /// <summary>
        /// Get sale receipt by id
        /// </summary>
        /// <param name="id">Id of sale receipt</param>
        /// <returns>Sale receipt</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Sale receipt not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var res  = saleReceipts.Get(id);

            if (res is null)
                return NotFound();

            return Ok(new SaleReceiptDTO(res));
        }

        /// <summary>
        /// Create new sale receipt
        /// </summary>
        /// <param name="saleReceipt">Sale receipt data</param>
        /// <returns>Id of new sale receipt</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid input</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] SaleReceiptDTO saleReceipt)
        {
            if (saleReceipt is null)
                return BadRequest();

            SaleReceipt receipt = saleReceipt.GetEntity();
            saleReceipts.Create(receipt);

            return Ok(receipt.Id);
        }

        /// <summary>
        /// Update sale receipt
        /// </summary>
        /// <param name="id">Id of sale receipt</param>
        /// <param name="saleReceipt">New sale receipt data</param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Sale receipt not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] SaleReceiptDTO saleReceipt)
        {
            SaleReceipt receipt = saleReceipts.Get(id);

            if (receipt is null)
                return NotFound();

            receipt.Fio = saleReceipt.Fio;
            receipt.DateOfPurchase = new DateOnly(saleReceipt.DateOfPurchase.Year, saleReceipt.DateOfPurchase.Month, saleReceipt.DateOfPurchase.Day);
            receipt.ShopId = saleReceipt.ShopId;

            saleReceipts.Update(receipt);

            return Ok();
        }

        /// <summary>
        /// Delete sale receipt
        /// </summary>
        /// <param name="id">Id of sale receipt</param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Sale receipt not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            SaleReceipt receipt = saleReceipts.Get(id);

            if (receipt is null)
                return NotFound();

            saleReceipts.Delete(receipt);

            return Ok();
        }

        /// <summary>
        /// Get sale receipts from shop
        /// </summary>
        /// <param name="shopId">Id of shop</param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Sale receipts or shop not found</response>
        [HttpGet("findByShop")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSaleReceiptsFromShop([FromQuery] int shopId)
        {
            var res = saleReceipts.GetAllFromShop(shopId);

            if (res is null)
                return NotFound();

            return Ok(res.Select(sr => new SaleReceiptWithSumCostDTO(sr) { SummaryCost = sr.SummaryCost.HasValue ? sr.SummaryCost.Value : 0}));
        }

        /// <summary>
        /// Get products from sale receipt
        /// </summary>
        /// <param name="saleReceiptId"></param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Sale receipt not found</response>
        [HttpGet("{saleReceiptId}/positions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSaleReceiptPositionsFromSaleReceipt(int saleReceiptId)
        {
            var saleReceipt = saleReceipts.Get(saleReceiptId);

            if (saleReceipt is null)
                return NotFound();

            var res = saleReceiptPositions.GetAllFromSaleReceipt(saleReceipt);

            return Ok(res.Select(srp => new ProductWithCostDTO(srp)));
        }

        /// <summary>
        /// Get sale receipt positions
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        [HttpGet("/positions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSaleReceiptPositions()
        {
            return Ok(saleReceiptPositions.GetAll().Select(srp => new SaleReceiptPositionDTO(srp)));
        }

        /// <summary>
        /// Get sale receipt position
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Sale receipt position not found</response>
        [HttpGet("/positions/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSaleReceiptPosition(int id)
        {
            var saleReceiptPos = saleReceiptPositions.Get(id);

            if (saleReceiptPos is null)
                return NotFound();

            return Ok(new SaleReceiptPositionDTO(saleReceiptPos));
        }

        /// <summary>
        /// Create new sale receipt position
        /// </summary>
        /// <param name="saleReceiptPosition"></param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid input</response>
        [HttpPost("/positions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostSaleReceiptPosition([FromBody] SaleReceiptPositionDTO saleReceiptPosition)
        {
            if (saleReceiptPosition is null)
                return BadRequest();

            var srp = saleReceiptPosition.GetEntity();
            saleReceiptPositions.Create(srp);

            return Ok(srp.Id);
        }

        /// <summary>
        /// Update sale receipt position
        /// </summary>
        /// <param name="id"></param>
        /// <param name="saleReceiptPositionDTO"></param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Sale receipt position not found</response>
        [HttpPut("/positions/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PutSaleReceiptPosition(int id, [FromBody] SaleReceiptPositionDTO saleReceiptPositionDTO)
        {
            var saleReceiptPos = saleReceiptPositions.Get(id);

            if (saleReceiptPos is null)
                return NotFound();

            saleReceiptPos.SaleReceiptId = saleReceiptPositionDTO.SaleReceiptId;
            saleReceiptPos.AvailabilityId = saleReceiptPositionDTO.AvailabilityId;

            saleReceiptPositions.Update(saleReceiptPos);

            return Ok();
        }

        /// <summary>
        /// Delete sale receipt position
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Sale receipt position not found</response>
        [HttpDelete("/positions/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteSaleReceiptPosition(int id)
        {
            var saleReceiptPos = saleReceiptPositions.Get(id);

            if (saleReceiptPos is null)
                return NotFound();

            saleReceiptPositions.Delete(saleReceiptPos);

            return Ok();
        }
    }
}
