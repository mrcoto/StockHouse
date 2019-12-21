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

        [Fact]
        public void Test_Should_Return_Physical_Stock_With_Warehouse_1_And_Product_1()
        {
            Assert.Equal(50, GetProductStock(productId: 1, warehouseId: 1));
        }

        [Fact]
        public void Test_Should_Return_Physical_Stock_With_Warehouse_1_And_Product_2()
        {
            Assert.Equal(100, GetProductStock(productId: 2, warehouseId: 1));
        }

        [Fact]
        public void Test_Should_Return_Physical_Stock_With_Warehouse_1_And_Product_3()
        {
            Assert.Equal(50, GetProductStock(productId: 3, warehouseId: 1));
        }

        [Fact]
        public void Test_Should_Return_Physical_Stock_With_Warehouse_1_And_Product_4()
        {
            Assert.Equal(20, GetProductStock(productId: 4, warehouseId: 1));
        }

        [Fact]
        public void Test_Should_Return_Logical_Stock_With_Warehouse_1_And_Product_5()
        {
            Assert.Equal(20, GetProductStock(productId: 5, warehouseId: 1));
        }

        [Fact]
        public void Test_Should_Return_Logical_Stock_With_Warehouse_1_And_Product_6()
        {
            Assert.Equal(16, GetProductStock(productId: 6, warehouseId: 1));
        }

        [Fact]
        public void Test_Should_Return_Logical_Stock_With_Warehouse_1_And_Product_7()
        {
            Assert.Equal(10, GetProductStock(productId: 7, warehouseId: 1));
        }

        [Fact]
        public void Test_Should_Return_Logical_Stock_With_Warehouse_1_And_Product_8()
        {
            Assert.Equal(5, GetProductStock(productId: 8, warehouseId: 1));
        }

        [Fact]
        public void Test_Should_Return_Physical_Stock_With_Warehouse_2_And_Product_1()
        {
            Assert.Equal(16, GetProductStock(productId: 1, warehouseId: 2));
        }

        [Fact]
        public void Test_Should_Return_Physical_Stock_With_Warehouse_2_And_Product_2()
        {
            Assert.Equal(0, GetProductStock(productId: 2, warehouseId: 2));
        }

        [Fact]
        public void Test_Should_Return_Physical_Stock_With_Warehouse_2_And_Product_3()
        {
            Assert.Equal(55, GetProductStock(productId: 3, warehouseId: 2));
        }

        [Fact]
        public void Test_Should_Return_Physical_Stock_With_Warehouse_2_And_Product_4()
        {
            Assert.Equal(0, GetProductStock(productId: 4, warehouseId: 2));
        }

        [Fact]
        public void Test_Should_Return_Logical_Stock_With_Warehouse_2_And_Product_5()
        {
            Assert.Equal(0, GetProductStock(productId: 5, warehouseId: 2));
        }

        [Fact]
        public void Test_Should_Return_Logical_Stock_With_Warehouse_2_And_Product_6()
        {
            Assert.Equal(18, GetProductStock(productId: 6, warehouseId: 2));
        }

        [Fact]
        public void Test_Should_Return_Logical_Stock_With_Warehouse_2_And_Product_7()
        {
            Assert.Equal(0, GetProductStock(productId: 7, warehouseId: 2));
        }

        [Fact]
        public void Test_Should_Return_Logical_Stock_With_Warehouse_2_And_Product_8()
        {
            Assert.Equal(0, GetProductStock(productId: 8, warehouseId: 2));
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