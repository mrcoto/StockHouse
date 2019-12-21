using System;
using System.Collections.Generic;
using StockHouse.Src.DB;
using StockHouse.Src.Examples.Example4;
using Xunit;

namespace StockHouseTest.Src.Examples.Example4
{
    public class GetProductsBySearchTokenServiceTest
    {

        [Fact]
        public void Test_Should_Return_Empty_On_Non_Match_Products()
        {
            var response = GetProductsBySearchToken("non handled search token");
            Assert.Equal(0, response.Count);
        }

        [Fact]
        public void Test_Should_Return_Products_For_Matching_Search_Token()
        {
            var response = GetProductsBySearchToken("game");
            Assert.Equal(3, response.Count);

            Assert.True(IsIdInProductInfoList(1, response));
            Assert.True(IsIdInProductInfoList(5, response));
            Assert.True(IsIdInProductInfoList(8, response));
        }

        [Fact]
        public void Test_Should_Return_All_Products_Composition_With_Empty_Search_Token()
        {
            var response = GetProductsBySearchToken("");
            Assert.Equal(8, response.Count);

            Assert.True(IsIdInProductInfoList(1, response));
            Assert.True(IsIdInProductInfoList(2, response));
            Assert.True(IsIdInProductInfoList(3, response));
            Assert.True(IsIdInProductInfoList(4, response));
            Assert.True(IsIdInProductInfoList(5, response));
            Assert.True(IsIdInProductInfoList(6, response));
            Assert.True(IsIdInProductInfoList(7, response));
            Assert.True(IsIdInProductInfoList(8, response));
        }

        private bool IsIdInProductInfoList(int id, List<ProductInfo> list)
        {
            return list.Exists(e => e.Id == id);
        }

        private List<ProductInfo> GetProductsBySearchToken(String searchToken)
        {
            using(var context = new StockHouseContext())
            {
                var getProductsBySearchTokenService = new GetProductsBySearchTokenService(context);
                return getProductsBySearchTokenService.Search(searchToken);
            }
        }
    }
}