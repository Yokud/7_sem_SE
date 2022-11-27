using DBLib.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class ShopsRepositoryTests
    {
        [Fact]
        public void TestGet()
        {
            IShopsRepository rep = new PgSQLShopsRepository();

            Shop shop = rep.Get(1);

            Assert.Equal("Похороны", shop.Name);
        }

        [Fact]
        public void TestCreate()
        {
            IShopsRepository rep = new PgSQLShopsRepository();
            Shop newShop = new Shop("123", "123");

            rep.Create(newShop);

            Assert.Equal("123", rep.GetAll().First(x => x.Name == "123").Name);
        }

        [Fact]
        public void TestUpdate() 
        {
            IShopsRepository rep = new PgSQLShopsRepository();
            Shop newShop = new Shop("123", "123");

            newShop.Name = "456";
            rep.Update(newShop);

            Assert.Equal("456", rep.GetAll().Where(x => x.Name == "456").First().Name);
        }

        [Fact]
        public void TestDelete() 
        {
            IShopsRepository rep = new PgSQLShopsRepository();
            Shop newShop = new Shop("123", "123");

            rep.Delete(newShop);

            Assert.Equal(Array.Empty<Shop>(), rep.GetAll().Where(x => x.Name == "456").ToArray());
        }
    }
}
