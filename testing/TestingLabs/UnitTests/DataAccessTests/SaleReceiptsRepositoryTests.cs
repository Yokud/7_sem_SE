using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class SaleReceiptsRepositoryTests
    {
        SaleReceiptBuilder builder = new SaleReceiptBuilder();

        [Fact]
        public void TestGet()
        {
            ISaleReceiptsRepository rep = new PgSQLSaleReceiptsRepository();
            SaleReceipt saleReceipt = builder.GetTestSample().Build();

            SaleReceipt sr = rep.Get(saleReceipt.Id);

            Assert.Equal(saleReceipt, sr);
        }

        [Fact]
        public void TestCreate()
        {
            ISaleReceiptsRepository rep = new PgSQLSaleReceiptsRepository();
            SaleReceipt newSr = builder.CreateTestSample().Build();

            rep.Create(newSr);
            SaleReceipt createdSR = rep.Get(newSr.Id);

            Assert.Equal(newSr, createdSR);
        }

        [Fact]
        public void TestUpdate()
        {
            ISaleReceiptsRepository rep = new PgSQLSaleReceiptsRepository();
            SaleReceipt updSr = builder.UpdateTestSample().Build();

            rep.Create(updSr);
            updSr.Fio = "456";
            rep.Update(updSr);
            SaleReceipt updatedSR = rep.Get(updSr.Id);

            Assert.Equal(updSr, updatedSR);
        }

        [Fact]
        public void TestDelete()
        {
            ISaleReceiptsRepository rep = new PgSQLSaleReceiptsRepository();
            SaleReceipt delSr = builder.DeleteTestSample().Build();

            rep.Create(delSr);
            rep.Delete(delSr);

            Assert.Null(rep.Get(delSr.Id));
        }
    }
}
