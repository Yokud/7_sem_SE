using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class ProductsRepositoryTests
    {
        ProductBuilder builder = new ProductBuilder();

        [Fact]
        public void TestGet()
        {
            IProductsRepository rep = new PgSQLProductsRepository();
            Product expectedProduct = builder.GetTestSample().Build();

            Product prod = rep.Get(expectedProduct.Id);
            
            Assert.Equal(expectedProduct, prod);          
        }

        [Fact]
        public void TestCreate()
        {
            IProductsRepository rep = new PgSQLProductsRepository();
            Product newProduct = builder.CreateTestSample().Build();
            
            rep.Create(newProduct);
            Product createdProduct = rep.Get(newProduct.Id);
            
            Assert.Equal(newProduct, createdProduct);
        }

        [Fact]
        public void TestUpdate()
        {
            IProductsRepository rep = new PgSQLProductsRepository();
            Product updProd = builder.UpdateTestSample().Build();

            rep.Create(updProd);
            updProd.Name = "456";
            rep.Update(updProd);
            Product updatedProd = rep.Get(updProd.Id);

            Assert.Equal(updProd, updatedProd);
        }

        [Fact]
        public void TestDelete()
        {
            IProductsRepository rep = new PgSQLProductsRepository();
            Product delProd = builder.DeleteTestSample().Build();

            rep.Create(delProd);
            rep.Delete(delProd);

            Assert.Null(rep.Get(delProd.Id));
        }

        [Fact]
        public void TestGetAllFromShop()
        {
            IProductsRepository rep = new PgSQLProductsRepository();
            Shop shop = new ShopBuilder().DeleteTestSample().Build();

            var res = rep.GetAllFromShop(shop);

            Assert.Empty(res);
        }
    }
}
