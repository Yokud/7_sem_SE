using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.BuildersNFabrics
{
    internal class SaleReceiptBuilder
    {
        SaleReceipt saleReceipt = new SaleReceipt(string.Empty, DateOnly.MaxValue, 0);

        public SaleReceiptBuilder GetTestSample() 
        {
            saleReceipt.Id = 1;
            saleReceipt.Fio = "Ратибор Димитриевич Михеев";
            saleReceipt.DateOfPurchase = new DateOnly(2021, 11, 13);
            saleReceipt.ShopId = 629;

            return this;
        }

        public SaleReceiptBuilder CreateTestSample()
        {
            saleReceipt.Fio = "Император Человечества";
            saleReceipt.DateOfPurchase = new DateOnly(2021, 11, 14);
            saleReceipt.ShopId = 555;

            return this;
        }

        public SaleReceiptBuilder UpdateTestSample()
        {
            saleReceipt.Fio = "Аномалокарис";
            saleReceipt.DateOfPurchase = new DateOnly(2022, 1, 3);
            saleReceipt.ShopId = 666;

            return this;
        }

        public SaleReceiptBuilder DeleteTestSample()
        {
            saleReceipt.Fio = "Не знаю";
            saleReceipt.DateOfPurchase = new DateOnly(2021, 11, 15);
            saleReceipt.ShopId = 777;

            return this;
        }

        public SaleReceipt Build() 
        { 
            return saleReceipt;
        }
    }
}
