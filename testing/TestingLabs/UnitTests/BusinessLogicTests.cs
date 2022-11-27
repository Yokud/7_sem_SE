namespace UnitTests
{
    public class BusinessLogicTests
    {
        [Fact]
        public void TestLinearSpline()
        {
            List<WeightPoint> data = new List<WeightPoint>
            {
                new WeightPoint(0, 0),
                new WeightPoint(1, 1),
                new WeightPoint(2, 1.5),
                new WeightPoint(3, 2.2),
                new WeightPoint(4, 3),
                new WeightPoint(5, 5)
            };
            BaseTrendLine line = new PolynomialTrendLine();

            var res = line.GetCoefs(data);

            Assert.Equal(res.Select(x => Math.Round(x, 3)), new List<double>() { -0.148, 0.906 });
        }

        [Fact]
        public void TestQuadraticSpline()
        {
            List<WeightPoint> data = new List<WeightPoint>
            {
                new WeightPoint(0, 1),
                new WeightPoint(1, 1),
                new WeightPoint(2, 0.7),
                new WeightPoint(3, 1),
                new WeightPoint(4, 3),
                new WeightPoint(5, 4)
            };
            BaseTrendLine line = new PolynomialTrendLine();

            var res = line.GetCoefs(data);

            Assert.Equal(res.Select(x => Math.Round(x, 3)), new List<double>() { 1.107, -0.659, 0.254 });
        }

        [Fact]
        public void TestCubicSpline()
        {
            List<WeightPoint> data = new List<WeightPoint>
            {
                new WeightPoint(0, 0),
                new WeightPoint(1, 2),
                new WeightPoint(2, 1),
                new WeightPoint(3, 1.2),
                new WeightPoint(4, 3),
                new WeightPoint(5, 4)
            };
            BaseTrendLine line = new PolynomialTrendLine();

            var res = line.GetCoefs(data);

            Assert.Equal(res.Select(x => Math.Round(x, 3)), new List<double>() { 0.24, 1.657, -0.737, 0.113 });
        }

        [Fact]
        public void TestDirectLine()
        {
            List<WeightPoint> data = new List<WeightPoint>
            {
                new WeightPoint(0, 0),
                new WeightPoint(1, 1),
                new WeightPoint(2, 2),
                new WeightPoint(3, 3),
                new WeightPoint(4, 4),
                new WeightPoint(5, 5)
            };
            BaseTrendLine line = new PolynomialTrendLine();

            var res = line.GetCoefs(data);

            Assert.Equal(res.Select(x => Math.Round(x, 3)), new List<double>() { 0, 1 });
        }

        [Fact]
        public void TestMaxExtremums()
        {
            List<WeightPoint> data = new List<WeightPoint>
            {
                new WeightPoint(0, 0),
                new WeightPoint(1, -1),
                new WeightPoint(2, 1),
                new WeightPoint(3, -1),
                new WeightPoint(4, 1),
                new WeightPoint(5, -1),
                new WeightPoint(6, 0),
                new WeightPoint(7, -1),
                new WeightPoint(8, 1),
                new WeightPoint(9, -1),
                new WeightPoint(10, 1),
                new WeightPoint(11, -1)
            };
            BaseTrendLine line = new PolynomialTrendLine();

            var res = line.GetCoefs(data);

            Assert.Equal(res.Select(x => Math.Round(x, 3)), new List<double>() { -0.108, -1.194, 1.180, -0.382, 0.053, -0.003, 0 });
        }

        [Fact]
        public void TestEmptyData()
        {
            List<WeightPoint> data = new List<WeightPoint>();
            BaseTrendLine line = new PolynomialTrendLine();

            var res = line.GetCoefs(data);

            Assert.Null(res);
        }

        [Fact]
        public void TestNullData()
        {
            BaseTrendLine line = new PolynomialTrendLine();

            var res = line.GetCoefs(null);

            Assert.Null(res);
        }
    }
}