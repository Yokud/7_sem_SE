using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.BuildersNFabrics
{
    internal class SplinesFabric
    {
        public IEnumerable<WeightPoint> CreateLinearSpline()
        {
            return new List<WeightPoint>
            {
                new WeightPoint(0, 0),
                new WeightPoint(1, 1),
                new WeightPoint(2, 1.5),
                new WeightPoint(3, 2.2),
                new WeightPoint(4, 3),
                new WeightPoint(5, 5)
            };
        }

        public IEnumerable<WeightPoint> CreateQuadraticSpline() 
        {
            return new List<WeightPoint>
            {
                new WeightPoint(0, 1),
                new WeightPoint(1, 1),
                new WeightPoint(2, 0.7),
                new WeightPoint(3, 1),
                new WeightPoint(4, 3),
                new WeightPoint(5, 4)
            };
        }

        public IEnumerable<WeightPoint> CreateCubicSpline()
        {
            return new List<WeightPoint>
            {
                new WeightPoint(0, 0),
                new WeightPoint(1, 2),
                new WeightPoint(2, 1),
                new WeightPoint(3, 1.2),
                new WeightPoint(4, 3),
                new WeightPoint(5, 4)
            };
        }

        public IEnumerable<WeightPoint> CreateDirectionLine()
        {
            return new List<WeightPoint>
            {
                new WeightPoint(0, 0),
                new WeightPoint(1, 1),
                new WeightPoint(2, 2),
                new WeightPoint(3, 3),
                new WeightPoint(4, 4),
                new WeightPoint(5, 5)
            };
        }

        public IEnumerable<WeightPoint> CreateSplineWithMaxExtremums()
        {
            return new List<WeightPoint>
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
        }

        public IEnumerable<WeightPoint> CreateEmptySpline() => new List<WeightPoint>();
    }
}
