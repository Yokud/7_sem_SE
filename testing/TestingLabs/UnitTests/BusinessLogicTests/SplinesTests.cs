using UnitTests.BuildersNFabrics;

namespace UnitTests
{
    public class SplinesTests
    {
        SplinesFabric splinesFabric;
        CoefsFabric coefsFabric;

        public SplinesTests() 
        {
            splinesFabric = new SplinesFabric();
            coefsFabric = new CoefsFabric();
        }

        [Fact]
        public void TestLinearSpline()
        {
            IEnumerable<WeightPoint> data = splinesFabric.CreateLinearSpline();
            IEnumerable<double> expectedCoefs = coefsFabric.LinearSplineCoefs();
            BaseTrendLine line = new PolynomialTrendLine();

            var res = line.GetCoefs(data);

            Assert.Equal(expectedCoefs, res.Select(x => Math.Round(x, 3)));
        }

        [Fact]
        public void TestQuadraticSpline()
        {
            IEnumerable<WeightPoint> data = splinesFabric.CreateQuadraticSpline();
            IEnumerable<double> expectedCoefs = coefsFabric.QuadraticSplineCoefs();
            BaseTrendLine line = new PolynomialTrendLine();

            var res = line.GetCoefs(data);

            Assert.Equal(expectedCoefs, res.Select(x => Math.Round(x, 3)));
        }

        [Fact]
        public void TestCubicSpline()
        {
            IEnumerable<WeightPoint> data = splinesFabric.CreateCubicSpline();
            IEnumerable<double> expectedCoefs = coefsFabric.CubicSplineCoefs();
            BaseTrendLine line = new PolynomialTrendLine();

            var res = line.GetCoefs(data);

            Assert.Equal(expectedCoefs, res.Select(x => Math.Round(x, 3)));
        }

        [Fact]
        public void TestDirectLine()
        {
            IEnumerable<WeightPoint> data = splinesFabric.CreateDirectionLine();
            IEnumerable<double> expectedCoefs = coefsFabric.DirectLineCoefs();
            BaseTrendLine line = new PolynomialTrendLine();

            var res = line.GetCoefs(data);

            Assert.Equal(expectedCoefs, res.Select(x => Math.Round(x, 3)));
        }

        [Fact]
        public void TestMaxExtremums()
        {
            IEnumerable<WeightPoint> data = splinesFabric.CreateSplineWithMaxExtremums();
            IEnumerable<double> expectedCoefs = coefsFabric.MaxExtremumsCoefs();
            BaseTrendLine line = new PolynomialTrendLine();

            var res = line.GetCoefs(data);

            Assert.Equal(expectedCoefs, res.Select(x => Math.Round(x, 3)));
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