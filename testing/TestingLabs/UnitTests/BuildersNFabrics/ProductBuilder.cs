using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.BuildersNFabrics
{
    internal class ProductBuilder
    {
        Product product = new Product(0, string.Empty, string.Empty);

        public ProductBuilder GetTestSample()
        {
            product.Id = 1;
            product.Name = "Коляска Adamex Barletta 2 in 1";
            product.ProductType = "Коляски";

            return this;
        }

        public ProductBuilder CreateTestSample()
        {
            product.Id = 1;
            product.Name = "Бейнблейд 11 стволов";
            product.ProductType = "Танки";

            return this;
        }

        public ProductBuilder UpdateTestSample()
        {
            product.Id = 1;
            product.Name = "Лазган";
            product.ProductType = "Оружие";

            return this;
        }

        public ProductBuilder DeleteTestSample()
        {
            product.Id = 1;
            product.Name = "Ересь Хоруса";
            product.ProductType = "Ересь";

            return this;
        }

        public Product Build()
        {
            return product;
        }
    }
}
