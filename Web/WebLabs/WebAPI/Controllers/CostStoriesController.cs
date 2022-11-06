using DBLib.Models;
using DBLib.SysEntities;
using Microsoft.AspNetCore.Mvc;
using TrendLineLib;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/coststories")]
    [ApiController]
    public class CostStoriesController : ControllerBase
    {
        ICostStoryRepository costStories;
        BaseTrendLine trend;

        public CostStoriesController(ICostStoryRepository costStoryRepository, BaseTrendLine trendLine)
        {
            costStories = costStoryRepository;

            trend = trendLine;
        }

        /// <summary>
        /// Return all cost stories units
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CostStoryDTO>))]
        public IActionResult Get()
        {
            return Ok(costStories.GetAll().Select(cs => new CostStoryDTO(cs)));
        }

        /// <summary>
        /// Return cost story unit
        /// </summary>
        /// <param name="id">Id of cost story unit</param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Cost story unit not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type =typeof(CostStoryDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var res = costStories.Get(id);

            if (res is null)
                return NotFound();

            return Ok(new CostStoryDTO(res));
        }

        /// <summary>
        /// Create new cost story unit
        /// </summary>
        /// <param name="costStory">New cost story unit</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid input</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] CostStoryDTO costStory)
        {
            if (costStory is null)
                return BadRequest();

            CostStory storyEntity = costStory.GetEntity();

            costStories.Create(storyEntity);

            return Ok(storyEntity.Id);
        }

        /// <summary>
        /// Update cost story unit
        /// </summary>
        /// <param name="id">Id of cost story unit</param>
        /// <param name="costStory">New data of cost story unit</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Cost story unit not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] CostStoryDTO costStory)
        {
            CostStory story = costStories.Get(id);

            if (story is null)
                return NotFound();

            story.Year = costStory.Year;
            story.Month = costStory.Month;
            story.Cost = costStory.Cost;
            story.AvailabilityId = costStory.AvailabilityId;

            costStories.Update(story);

            return Ok();
        }

        /// <summary>
        /// Delete cost story unit
        /// </summary>
        /// <param name="id">Id of cost story unit</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Cost story unit not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            CostStory story = costStories.Get(id);

            if (story is null)
                return NotFound();

            costStories.Delete(story);

            return Ok();
        }

        /// <summary>
        /// Return full cost story of product in the shop
        /// </summary>
        /// <param name="shopId">Id of shop</param>
        /// <param name="productId">Id of product</param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Cost story not found</response>
        [HttpGet("{shopId}/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFullCostStory(int shopId, int productId)
        {
            var res = costStories.GetFullCostStory(shopId, productId);

            if (res is null)
                return NotFound();

            return Ok(res);
        }

        /// <summary>
        /// Return full cost story trend line of product in the shop
        /// </summary>
        /// <param name="shopId">Id of shop</param>
        /// <param name="productId">Id of product</param>
        /// <returns></returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Cost story not found</response>
        [HttpGet("{shopId}/{productId}/trendline")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFullCostStoryTrendLine(int shopId, int productId)
        {
            var res = costStories.GetFullCostStory(shopId, productId);

            if (res is null)
                return NotFound();

            trend.GetCoefs(res);
            IEnumerable<Point> points = trend.GetLinePoints(res);

            return Ok(points);
        }
    }

    internal static class CostStoryPoints
    {
        public static IEnumerable<Point> FromCostStory(IEnumerable<CostStory> cs)
        {
            cs = cs.OrderBy(x => new DateOnly(x.Year, x.Month, 1));
            List<int> costs = cs.ToList().Select(x => x.Cost).ToList();
            List<DateOnly> dates = cs.Select(x => new DateOnly(x.Year, x.Month, 1)).ToList();

            List<Point> points = new List<Point>();

            for (int i = 0; i < Math.Max(costs.Count, dates.Count); i++)
            {
                points.Add(new Point(i + 1, costs[i]));
            }

            return points;
        }

        public static IEnumerable<double> GetCoefs(this BaseTrendLine line, IEnumerable<CostStory> costStories)
        {
            var points = FromCostStory(costStories);

            return line.GetCoefs(points.ToList());
        }

        public static IEnumerable<Point> GetLinePoints(this BaseTrendLine line, IEnumerable<CostStory> costStories)
        {
            var points = FromCostStory(costStories);

            return line.GetLinePoints(points);
        }
    }
}
