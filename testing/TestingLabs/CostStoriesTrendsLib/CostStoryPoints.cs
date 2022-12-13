using DBLib.Models;
using TrendLineLib;

namespace CostStoriesTrendsLib
{
    internal static class CostStoryPoints
    {
        public static IEnumerable<double> GetCoefsFromCostStory(this BaseTrendLine line, ICostStoryRepository cs)
        {
            var csSorted = cs.GetAll().OrderBy(x => new DateOnly(x.Year, x.Month, 1));
            List<int> costs = csSorted.ToList().Select(x => x.Cost).ToList();
            List<DateOnly> dates = csSorted.Select(x => new DateOnly(x.Year, x.Month, 1)).ToList();

            List<WeightPoint> points = new List<WeightPoint>();

            for (int i = 0; i < Math.Max(costs.Count, dates.Count); i++)
            {
                points.Add(new WeightPoint(i + 1, costs[i]));
            }

            return line.GetCoefs(points);
        }

        public static IEnumerable<WeightPoint>? GetLinePointsFromCostStory(this BaseTrendLine line, ICostStoryRepository cs)
        {
            var csSorted = cs.GetAll().OrderBy(x => new DateOnly(x.Year, x.Month, 1));
            List<int> costs = csSorted.ToList().Select(x => x.Cost).ToList();
            List<DateOnly> dates = csSorted.Select(x => new DateOnly(x.Year, x.Month, 1)).ToList();

            List<WeightPoint> points = new List<WeightPoint>();

            for (int i = 0; i < Math.Max(costs.Count, dates.Count); i++)
            {
                points.Add(new WeightPoint(i + 1, costs[i]));
            }

            return line.GetLinePoints(points);
        }
    }
}