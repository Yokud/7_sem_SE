using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.BuildersNFabrics
{
    internal class CoefsFabric
    {
        public IEnumerable<double> LinearSplineCoefs()
        {
            return new List<double>() { -0.148, 0.906 };
        }

        public IEnumerable<double> QuadraticSplineCoefs() 
        {
            return new List<double>() { 1.107, -0.659, 0.254 };
        }

        public IEnumerable<double> CubicSplineCoefs()
        {
            return new List<double>() { 0.24, 1.657, -0.737, 0.113 };
        }

        public IEnumerable<double> DirectLineCoefs()
        {
            return new List<double>() { 0, 1 };
        }

        public IEnumerable<double> MaxExtremumsCoefs()
        {
            return new List<double>() { -0.108, -1.194, 1.180, -0.382, 0.053, -0.003, 0 };
        }
    }
}
