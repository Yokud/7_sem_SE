using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class SaleReceiptPositionsRepositoryTests
    {
        [Fact]
        public void TestGet()
        {
            ISaleReceiptPositionsRepository rep = new PgSQLSaleReceiptPositionsRepository();

            SaleReceiptPosition sr = rep.Get(1);

            Assert.Equal(6801, sr.AvailabilityId);
        }

        [Fact]
        public void TestCreate()
        {
            ISaleReceiptPositionsRepository rep = new PgSQLSaleReceiptPositionsRepository();
            SaleReceiptPosition newSr = new SaleReceiptPosition(123, 1);

            rep.Create(newSr);

            Assert.Equal(123, rep.GetAll().Where(x => x.Id == 10053).First().AvailabilityId);
        }

        [Fact]
        public void TestUpdate()
        {
            ISaleReceiptPositionsRepository rep = new PgSQLSaleReceiptPositionsRepository();
            SaleReceiptPosition newSr = new SaleReceiptPosition(123, 1);

            newSr.AvailabilityId = 456;
            rep.Update(newSr);

            Assert.Equal(456, rep.GetAll().Where(x => x.Id == 10053).First().AvailabilityId);
        }

        [Fact]
        public void TestDelete()
        {
            ISaleReceiptPositionsRepository rep = new PgSQLSaleReceiptPositionsRepository();
            SaleReceiptPosition newSr = new SaleReceiptPosition(123, 1);

            rep.Delete(newSr);

            Assert.Equal(Array.Empty<SaleReceiptPosition>(), rep.GetAll().Where(x => x.Id == 10053).ToArray());
        }
    }
}
