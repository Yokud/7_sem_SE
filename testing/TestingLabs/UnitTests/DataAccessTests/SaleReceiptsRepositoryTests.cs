using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class SaleReceiptsRepositoryTests
    {
        [Fact]
        public void TestGet()
        {
            ISaleReceiptsRepository rep = new PgSQLSaleReceiptsRepository();

            SaleReceipt sr = rep.Get(1);

            Assert.Equal("Полякова Ангелина Тимофеевна", sr.Fio);
        }

        [Fact]
        public void TestCreate()
        {
            ISaleReceiptsRepository rep = new PgSQLSaleReceiptsRepository();
            SaleReceipt newSr = new SaleReceipt("123", new DateOnly(1, 1, 1), 1);

            rep.Create(newSr);

            Assert.Equal("123", rep.GetAll().Where(x => x.Fio == "123").First().Fio);
        }

        [Fact]
        public void TestUpdate()
        {
            ISaleReceiptsRepository rep = new PgSQLSaleReceiptsRepository();
            SaleReceipt newSr = new SaleReceipt("123", new DateOnly(1, 1, 1), 1);

            newSr.Fio = "456";
            rep.Update(newSr);

            Assert.Equal("456", rep.GetAll().Where(x => x.Fio == "456").First().Fio);
        }

        [Fact]
        public void TestDelete()
        {
            ISaleReceiptsRepository rep = new PgSQLSaleReceiptsRepository();
            SaleReceipt newSr = new SaleReceipt("123", new DateOnly(1, 1, 1), 1);

            rep.Delete(newSr);

            Assert.Equal(Array.Empty<SaleReceipt>(), rep.GetAll().Where(x => x.Fio == "456").ToArray());
        }
    }
}
