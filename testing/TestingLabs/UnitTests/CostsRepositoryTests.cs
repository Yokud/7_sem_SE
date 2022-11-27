using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class CostsRepositoryTests
    {
        [Fact]
        public void TestGet()
        {
            ICostsRepository rep = new PgSQLCostsRepository();

            Cost c = rep.Get(1);

            Assert.Equal(5652, c.CostValue);
        }
    }
}
