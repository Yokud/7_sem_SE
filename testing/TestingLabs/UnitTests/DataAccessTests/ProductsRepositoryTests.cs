using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class ProductsRepositoryTests
    {
        [Fact]
        public void TestGet()
        {
            IProductsRepository rep = new PgSQLProductsRepository();

            Product prod = rep.Get(1);
            
            Assert.Equal("Коляска Adamex Barletta 2 in 1", prod.Name);

            // Create
            

            // Update
            

            // Delete
            
        }

        [Fact]
        public void TestCreate()
        {
            IProductsRepository rep = new PgSQLProductsRepository();
            Product newProd = new Product("123", "123");
            
            rep.Create(newProd);
            
            Assert.Equal("123", rep.GetAll().Where(x => x.Name == "123").First().Name);
        }

        [Fact]
        public void TestUpdate()
        {
            IProductsRepository rep = new PgSQLProductsRepository();
            Product newProd = new Product("123", "123");

            newProd.Name = "456";
            rep.Update(newProd);

            Assert.Equal("456", rep.GetAll().Where(x => x.Name == "456").First().Name);
        }

        [Fact]
        public void TestDelete()
        {
            IProductsRepository rep = new PgSQLProductsRepository();
            Product newProd = new Product("123", "123");

            rep.Delete(newProd);

            Assert.Equal(Array.Empty<Product>(), rep.GetAll().Where(x => x.Name == "456").ToArray());
        }
    }
}
