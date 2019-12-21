using System;
using StockHouse.Src.DB;
using StockHouse.Src.Examples.Example5;
using Xunit;

namespace StockHouseTest.Src.Examples.Example5
{
    public class ProductLogicalStockServiceTest
    {
        [Fact]
        public void Test_Should_Throw_InvalidOperationException_With_Not_Existing_Product()
        {
            Assert.Throws<InvalidOperationException>(() => GetProductStock(productId: -1, warehouseId: 1));
        }

        [Fact]
        public void Test_Should_Throw_InvalidOperationException_With_Not_Existing_Warehouse()
        {
            Assert.Throws<InvalidOperationException>(() => GetProductStock(productId: 1, warehouseId: -1));
        }

        [Theory]
        [InlineData(1, 1, 50)]
        [InlineData(1, 2, 100)]
        [InlineData(1, 3, 50)]
        [InlineData(1, 4, 20)]
        [InlineData(1, 5, 20)]
        [InlineData(1, 6, 16)]
        [InlineData(1, 7, 10)]
        [InlineData(1, 8, 5)]
        [InlineData(2, 1, 16)]
        [InlineData(2, 2, 0)]
        [InlineData(2, 3, 55)]
        [InlineData(2, 4, 0)]
        [InlineData(2, 5, 0)]
        [InlineData(2, 6, 18)]
        [InlineData(2, 7, 0)]
        [InlineData(2, 8, 0)]
        public void Test_Should_Return_Stock(int warehouseId, int productId, int stock)
        {
            Assert.Equal(stock, GetProductStock(productId, warehouseId));
        }

        private int GetProductStock(int productId, int warehouseId)
        {
            using(var context = new StockHouseContext())
            {
                var productLogicalStockService = new ProductLogicalStockService(context);
                return productLogicalStockService.GetStock(productId, warehouseId);
            }
        }
    }
}