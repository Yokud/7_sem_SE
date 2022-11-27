

namespace TrendLineLib
{
    public class WeightPoint
    {
        public WeightPoint(double x, double y, double p = 1)
        {
            X = x;
            Y = y;
            P = p;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double P { get; set; }
    }
}