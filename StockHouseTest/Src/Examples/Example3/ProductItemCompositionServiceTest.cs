using System.Collections.Generic;
using StockHouse.Src.DB;
using StockHouse.Src.Examples.Example3;
using Xunit;

namespace StockHouseTest.Src.Examples.Example3
{
    
    public class ProductItemCompositionServiceTest
    {

        [Fact]
        public void Test_Should_Return_Empty_Composition_On_Non_Existing_Product()
        {
            var response = GetItemComposition(-1);
            Assert.Equal(0, response.Count);
        }

        [Fact]
        public void Test_Should_Return_Empty_Composition_With_Product_ID_1()
        {
            var response = GetItemComposition(1);
            Assert.Equal(0, response.Count);
        }

        [Fact]
        public void Test_Should_Return_Item_Composition_With_Product_ID_7()
        {
            var response = GetItemComposition(7);
            Assert.Equal(4, response.Count);

            var composition = FindItemCompositionById(1, response);
            Assert.Equal(1, composition.Id);
            Assert.Equal("keyboardgamer", composition.AliasName);
            Assert.Equal("Corsair K95 RGB Platinum", composition.Name);
            Assert.Equal(4, composition.Quantity);

            composition = FindItemCompositionById(4, response);
            Assert.Equal(4, composition.Id);
            Assert.Equal("printer", composition.AliasName);
            Assert.Equal("Printer", composition.Name);
            Assert.Equal(2, composition.Quantity);

            composition = FindItemCompositionById(3, response);
            Assert.Equal(3, composition.Id);
            Assert.Equal("mouse", composition.AliasName);
            Assert.Equal("Generic Mouse", composition.Name);
            Assert.Equal(3, composition.Quantity);

            composition = FindItemCompositionById(2, response);
            Assert.Equal(2, composition.Id);
            Assert.Equal("keyboard", composition.AliasName);
            Assert.Equal("Generic Keyboard", composition.Name);
            Assert.Equal(3, composition.Quantity);
        }

        [Fact]
        public void Test_Should_Return_Item_Composition_With_Product_ID_8()
        {
            var response = GetItemComposition(8);
            Assert.Equal(2, response.Count);

            var composition = FindItemCompositionById(1, response);
            Assert.Equal(1, composition.Id);
            Assert.Equal("keyboardgamer", composition.AliasName);
            Assert.Equal("Corsair K95 RGB Platinum", composition.Name);
            Assert.Equal(10, composition.Quantity);

            composition = FindItemCompositionById(4, response);
            Assert.Equal(4, composition.Id);
            Assert.Equal("printer", composition.AliasName);
            Assert.Equal("Printer", composition.Name);
            Assert.Equal(3, composition.Quantity);
        }

        private ItemComposition FindItemCompositionById(int id, List<ItemComposition> list)
        {
            return list.Find(e => e.Id == id);
        }

        private List<ItemComposition> GetItemComposition(int id)
        {
            using(var context = new StockHouseContext())
            {
                var productItemCompositionService = new ProductItemCompositionService(context);
                return productItemCompositionService.GetItems(id);
            }
        }

    }
}