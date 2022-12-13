using DBLib.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.BuildersNFabrics;

namespace UnitTests
{
    public class ShopsRepositoryTests
    {
        ShopBuilder builder;

        public ShopsRepositoryTests() 
        {
            builder = new ShopBuilder();
        }

        [Fact]
        public void TestGet()
        {
            IShopsRepository rep = new PgSQLShopsRepository();
            Shop expectedShop = builder.GetTestSample().Build();

            Shop shop = rep.Get(expectedShop.Id);
            
            Assert.Equal(expectedShop, shop);
        }

        [Fact]
        public void TestCreate()
        {
            IShopsRepository rep = new PgSQLShopsRepository();
            Shop newShop = builder.CreateTestSample().Build();

            rep.Create(newShop);
            Shop createdShop = rep.Get(newShop.Id);

            Assert.Equal(newShop, createdShop);
        }

        [Fact]
        public void TestUpdate() 
        {
            IShopsRepository rep = new PgSQLShopsRepository();
            Shop updShop = builder.UpdateTestSample().Build();

            rep.Create(updShop);
            updShop.Name = "456";
            rep.Update(updShop);
            Shop updatedShop = rep.Get(updShop.Id);

            Assert.Equal(updShop, updatedShop);
        }

        [Fact]
        public void TestDelete() 
        {
            IShopsRepository rep = new PgSQLShopsRepository();
            Shop delShop = builder.DeleteTestSample().Build();

            rep.Create(delShop);
            rep.Delete(delShop);

            Assert.Null(rep.Get(delShop.Id));
        }
    }
}
