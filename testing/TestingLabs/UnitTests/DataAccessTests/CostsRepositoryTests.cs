using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class CostsRepositoryTests
    {
        CostBuilder builder = new CostBuilder();

        [Fact]
        public void TestGet()
        {
            ICostsRepository rep = new PgSQLCostsRepository();
            Cost cost = builder.GetTestSample().Build();

            Cost c = rep.Get(cost.AvailabilityId);

            Assert.Equal(cost, c);
        }
    }
}
