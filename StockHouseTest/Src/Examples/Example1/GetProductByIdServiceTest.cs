using System;
using StockHouse.Src.DB;
using StockHouse.Src.Examples.Example1;
using Xunit;

namespace StockHouseTest.Src.Examples.Example1
{
    public class GetProductByIdServiceTest
    {
        
        [Fact]
        public void Test_Should_Throw_InvalidOperationException_With_Not_In_DB_Product()
        {
            Assert.Throws<InvalidOperationException>(() => GetProductById(-1));
        }

        [Fact]
        public void Test_Should_Return_Data_Of_Product_With_ID_1()
        {
            var response = GetProductById(1);
            Assert.Equal("keyboardgamer", response.AliasName);
            Assert.Equal("Corsair K95 RGB Platinum", response.Name);
            Assert.False(response.HasComposition);
        }

        [Fact]
        public void Test_Should_Return_Data_Of_Product_With_ID_6()
        {
            var response = GetProductById(6);
            Assert.Equal("pack2", response.AliasName);
            Assert.Equal("Pack #2: 3 x Generic Mouse", response.Name);
            Assert.True(response.HasComposition);
        }

        private ProductDataDto GetProductById(int id)
        {
            using(var context = new StockHouseContext())
            {
                var getProductByIdService = new GetProductByIdService(context);
                return getProductByIdService.byId(id);
            }
        }

    }
}