using UnitTests.BuildersNFabrics;

namespace UnitTests
{
    public class BusinessLogicTests
    {
        SplinesFabric splinesFabric;

        public BusinessLogicTests() 
        {
            splinesFabric = new SplinesFabric();
        }

        [Fact]
        public void TestLinearSpline()
        {
            IEnumerable<WeightPoint> data = splinesFabric.CreateLinearSpline();
            BaseTrendLine line = new PolynomialTrendLine();

            var res = line.GetCoefs(data);

            Assert.Equal(new List<double>() { -0.148, 0.906 }, res.Select(x => Math.Round(x, 3)));
        }

        [Fact]
        public void TestQuadraticSpline()
        {
            IEnumerable<WeightPoint> data = splinesFabric.CreateQuadraticSpline();
            BaseTrendLine line = new PolynomialTrendLine();

            var res = line.GetCoefs(data);

            Assert.Equal(new List<double>() { 1.107, -0.659, 0.254 }, res.Select(x => Math.Round(x, 3)));
        }

        [Fact]
        public void TestCubicSpline()
        {
            IEnumerable<WeightPoint> data = splinesFabric.CreateCubicSpline();
            BaseTrendLine line = new PolynomialTrendLine();

            var res = line.GetCoefs(data);

            Assert.Equal(new List<double>() { 0.24, 1.657, -0.737, 0.113 }, res.Select(x => Math.Round(x, 3)));
        }

        [Fact]
        public void TestDirectLine()
        {
            IEnumerable<WeightPoint> data = splinesFabric.CreateDirectionLine();
            BaseTrendLine line = new PolynomialTrendLine();

            var res = line.GetCoefs(data);

            Assert.Equal(new List<double>() { 0, 1 }, res.Select(x => Math.Round(x, 3)));
        }

        [Fact]
        public void TestMaxExtremums()
        {
            IEnumerable<WeightPoint> data = splinesFabric.CreateSplineWithMaxExtremums();
            BaseTrendLine line = new PolynomialTrendLine();

            var res = line.GetCoefs(data);

            Assert.Equal(new List<double>() { -0.108, -1.194, 1.180, -0.382, 0.053, -0.003, 0 }, res.Select(x => Math.Round(x, 3)));
        }

        [Fact]
        public void TestEmptyData()
        {
            IEnumerable<WeightPoint> data = splinesFabric.CreateEmptySpline();
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