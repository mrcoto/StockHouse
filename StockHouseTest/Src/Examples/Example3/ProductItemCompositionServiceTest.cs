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
        public void Test_Should_Return_Empty_Composition_With_Product_ID_7_And_Level_1()
        {
            var response = GetItemComposition(7, 1);
            Assert.Equal(3, response.Count);

            var composition = FindProductCompositionById(5, response);
            Assert.Equal(5, composition.Id);
            Assert.Equal("pack1", composition.AliasName);     
            Assert.Equal("Pack #1: 2 x Keyboard Gamer & Printer", composition.Name);            
            Assert.Equal(2, composition.Quantity);            
            Assert.Equal(0, composition.Composition.Count);    

            composition = FindProductCompositionById(6, response);
            Assert.Equal(6, composition.Id);            
            Assert.Equal("pack2", composition.AliasName);            
            Assert.Equal("Pack #2: 3 x Generic Mouse", composition.Name);            
            Assert.Equal(1, composition.Quantity);            
            Assert.Equal(0, composition.Composition.Count);

            composition = FindProductCompositionById(2, response);
            Assert.Equal(2, composition.Id);            
            Assert.Equal("keyboard", composition.AliasName);            
            Assert.Equal("Generic Keyboard", composition.Name);            
            Assert.Equal(3, composition.Quantity);            
            Assert.Equal(0, composition.Composition.Count);              
        }

        [Fact]
        public void Test_Should_Return_Empty_Composition_With_Product_ID_7_And_Level_2()
        {
            var response = GetItemComposition(7, 2);
            Assert.Equal(3, response.Count);

            var composition = FindProductCompositionById(5, response);
            Assert.Equal(5, composition.Id);
            Assert.Equal("pack1", composition.AliasName);     
            Assert.Equal("Pack #1: 2 x Keyboard Gamer & Printer", composition.Name);            
            Assert.Equal(2, composition.Quantity);            
            Assert.Equal(2, composition.Composition.Count);    

            var childComposition = FindProductCompositionById(1, composition.Composition);
            Assert.Equal(1, childComposition.Id);            
            Assert.Equal("keyboardgamer", childComposition.AliasName);            
            Assert.Equal("Corsair K95 RGB Platinum", childComposition.Name);            
            Assert.Equal(2, childComposition.Quantity);            
            Assert.Equal(0, childComposition.Composition.Count);   

            childComposition = FindProductCompositionById(4, composition.Composition);
            Assert.Equal(4, childComposition.Id);            
            Assert.Equal("printer", childComposition.AliasName);            
            Assert.Equal("Printer", childComposition.Name);            
            Assert.Equal(1, childComposition.Quantity);            
            Assert.Equal(0, childComposition.Composition.Count); 

            composition = FindProductCompositionById(6, response);
            Assert.Equal(6, composition.Id);            
            Assert.Equal("pack2", composition.AliasName);            
            Assert.Equal("Pack #2: 3 x Generic Mouse", composition.Name);            
            Assert.Equal(1, composition.Quantity);            
            Assert.Equal(1, composition.Composition.Count);

            childComposition = FindProductCompositionById(3, composition.Composition);
            Assert.Equal(3, childComposition.Id);            
            Assert.Equal("mouse", childComposition.AliasName);            
            Assert.Equal("Generic Mouse", childComposition.Name);            
            Assert.Equal(3, childComposition.Quantity);            
            Assert.Equal(0, childComposition.Composition.Count);  

            composition = FindProductCompositionById(2, response);
            Assert.Equal(2, composition.Id);            
            Assert.Equal("keyboard", composition.AliasName);            
            Assert.Equal("Generic Keyboard", composition.Name);            
            Assert.Equal(3, composition.Quantity);            
            Assert.Equal(0, composition.Composition.Count);
        }

        [Fact]
        public void Test_Should_Return_Empty_Composition_With_Product_ID_7_And_Level_100()
        {
            var response = GetItemComposition(7, 100);
            Assert.Equal(3, response.Count);

            var composition = FindProductCompositionById(5, response);
            Assert.Equal(5, composition.Id);
            Assert.Equal("pack1", composition.AliasName);     
            Assert.Equal("Pack #1: 2 x Keyboard Gamer & Printer", composition.Name);            
            Assert.Equal(2, composition.Quantity);       
            Assert.Equal(2, composition.Composition.Count);    

            var childComposition = FindProductCompositionById(1, composition.Composition);
            Assert.Equal(1, childComposition.Id);            
            Assert.Equal("keyboardgamer", childComposition.AliasName);            
            Assert.Equal("Corsair K95 RGB Platinum", childComposition.Name);            
            Assert.Equal(2, childComposition.Quantity);            
            Assert.Equal(0, childComposition.Composition.Count);   

            childComposition = FindProductCompositionById(4, composition.Composition);
            Assert.Equal(4, childComposition.Id);            
            Assert.Equal("printer", childComposition.AliasName);            
            Assert.Equal("Printer", childComposition.Name);            
            Assert.Equal(1, childComposition.Quantity);            
            Assert.Equal(0, childComposition.Composition.Count); 

            composition = FindProductCompositionById(6, response);
            Assert.Equal(6, composition.Id);            
            Assert.Equal("pack2", composition.AliasName);            
            Assert.Equal("Pack #2: 3 x Generic Mouse", composition.Name);            
            Assert.Equal(1, composition.Quantity);            
            Assert.Equal(1, composition.Composition.Count);

            childComposition = FindProductCompositionById(3, composition.Composition);
            Assert.Equal(3, childComposition.Id);            
            Assert.Equal("mouse", childComposition.AliasName);            
            Assert.Equal("Generic Mouse", childComposition.Name);            
            Assert.Equal(3, childComposition.Quantity);            
            Assert.Equal(0, childComposition.Composition.Count);  

            composition = FindProductCompositionById(2, response);
            Assert.Equal(2, composition.Id);            
            Assert.Equal("keyboard", composition.AliasName);            
            Assert.Equal("Generic Keyboard", composition.Name);            
            Assert.Equal(3, composition.Quantity);            
            Assert.Equal(0, composition.Composition.Count);             
        }

        private ProductComposition FindProductCompositionById(int id, List<ProductComposition> list)
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