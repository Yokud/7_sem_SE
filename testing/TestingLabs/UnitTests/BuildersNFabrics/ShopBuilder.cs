using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.BuildersNFabrics
{
    internal class ShopBuilder
    {
        Shop shop = new Shop(0, string.Empty, string.Empty);

        public ShopBuilder GetTestSample()
        {
            shop.Id = 1;
            shop.Name = "Бабочка";
            shop.Description = "Ботинок изба забирать дошлый выразить подземный.";

            return this;
        }

        public ShopBuilder CreateTestSample() 
        {
            shop.Name = "123";
            shop.Description = "123";

            return this;
        }

        public ShopBuilder UpdateTestSample() 
        {
            shop.Name = "idk";
            shop.Description = "hz";

            return this;
        }

        public ShopBuilder DeleteTestSample()
        {
            shop.Name = "555";
            shop.Description = "123";

            return this;
        }

        public Shop Build()
        {
            return shop;
        }
    }
}
