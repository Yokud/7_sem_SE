using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.BuildersNFabrics
{
    internal class CostBuilder
    {
        Cost cost = new Cost(0, 0);

        public CostBuilder GetTestSample() 
        {
            cost.AvailabilityId = 280;
            cost.CostValue = 25139;

            return this;
        }

        public Cost Build()
        {
            return cost;
        }
    }
}
