using System;
using System.Collections.Generic;
using StockHouse.Src.DB;
using StockHouse.Src.Examples.Example2;
using Xunit;

namespace StockHouseTest.Src.Examples.Example2
{
    public class ProductTreeCompositionServiceTest
    {
        
        [Fact]
        public void Test_Should_Throw_ArgumentException_If_Level_Is_Negative()
        {
            Assert.Throws<ArgumentException>(() => GetProductComposition(1, -1));
        }

        [Fact]
        public void Test_Should_Throw_ArgumentException_If_Level_Is_Zero()
        {
            Assert.Throws<ArgumentException>(() => GetProductComposition(1, 0));
        }

        [Fact]
        public void Test_Should_Return_Empty_Composition_On_Non_Existing_Product()
        {
            var response = GetProductComposition(-1, 1);
            Assert.Empty(response);
        }

        [Fact]
        public void Test_Should_Return_Empty_Composition_With_Product_ID_1()
        {
            var response = GetProductComposition(1, 1);
            Assert.Empty(response);
        }

        [Fact]
        public void Test_Should_Return_Composition_With_Product_ID_7_And_Level_1()
        {
            var response = GetProductComposition(7, 1);
            Assert.Equal(3, response.Count);

            var composition = FindProductCompositionById(5, response);
            Assert.Equal(5, composition.Id);
            Assert.Equal("pack1", composition.AliasName);     
            Assert.Equal("Pack #1: 2 x Keyboard Gamer & Printer", composition.Name);            
            Assert.Equal(2, composition.Quantity);            
            Assert.Empty(composition.Composition);    

            composition = FindProductCompositionById(6, response);
            Assert.Equal(6, composition.Id);            
            Assert.Equal("pack2", composition.AliasName);            
            Assert.Equal("Pack #2: 3 x Generic Mouse", composition.Name);            
            Assert.Equal(1, composition.Quantity);            
            Assert.Empty(composition.Composition);

            composition = FindProductCompositionById(2, response);
            Assert.Equal(2, composition.Id);            
            Assert.Equal("keyboard", composition.AliasName);            
            Assert.Equal("Generic Keyboard", composition.Name);            
            Assert.Equal(3, composition.Quantity);            
            Assert.Empty(composition.Composition);              
        }

        [Fact]
        public void Test_Should_Return_Composition_With_Product_ID_7_And_Level_2()
        {
            var response = GetProductComposition(7, 2);
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
            Assert.Empty(childComposition.Composition);   

            childComposition = FindProductCompositionById(4, composition.Composition);
            Assert.Equal(4, childComposition.Id);            
            Assert.Equal("printer", childComposition.AliasName);            
            Assert.Equal("Printer", childComposition.Name);            
            Assert.Equal(1, childComposition.Quantity);            
            Assert.Empty(childComposition.Composition); 

            composition = FindProductCompositionById(6, response);
            Assert.Equal(6, composition.Id);            
            Assert.Equal("pack2", composition.AliasName);            
            Assert.Equal("Pack #2: 3 x Generic Mouse", composition.Name);            
            Assert.Equal(1, composition.Quantity);            
            Assert.Single(composition.Composition);

            childComposition = FindProductCompositionById(3, composition.Composition);
            Assert.Equal(3, childComposition.Id);            
            Assert.Equal("mouse", childComposition.AliasName);            
            Assert.Equal("Generic Mouse", childComposition.Name);            
            Assert.Equal(3, childComposition.Quantity);            
            Assert.Empty(childComposition.Composition);  

            composition = FindProductCompositionById(2, response);
            Assert.Equal(2, composition.Id);            
            Assert.Equal("keyboard", composition.AliasName);            
            Assert.Equal("Generic Keyboard", composition.Name);            
            Assert.Equal(3, composition.Quantity);            
            Assert.Empty(composition.Composition);
        }

        [Fact]
        public void Test_Should_Return_Composition_With_Product_ID_7_And_Level_100()
        {
            var response = GetProductComposition(7, 100);
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
            Assert.Empty(childComposition.Composition);   

            childComposition = FindProductCompositionById(4, composition.Composition);
            Assert.Equal(4, childComposition.Id);            
            Assert.Equal("printer", childComposition.AliasName);            
            Assert.Equal("Printer", childComposition.Name);            
            Assert.Equal(1, childComposition.Quantity);            
            Assert.Empty(childComposition.Composition); 

            composition = FindProductCompositionById(6, response);
            Assert.Equal(6, composition.Id);            
            Assert.Equal("pack2", composition.AliasName);            
            Assert.Equal("Pack #2: 3 x Generic Mouse", composition.Name);            
            Assert.Equal(1, composition.Quantity);            
            Assert.Single(composition.Composition);

            childComposition = FindProductCompositionById(3, composition.Composition);
            Assert.Equal(3, childComposition.Id);            
            Assert.Equal("mouse", childComposition.AliasName);            
            Assert.Equal("Generic Mouse", childComposition.Name);            
            Assert.Equal(3, childComposition.Quantity);            
            Assert.Empty(childComposition.Composition);  

            composition = FindProductCompositionById(2, response);
            Assert.Equal(2, composition.Id);            
            Assert.Equal("keyboard", composition.AliasName);            
            Assert.Equal("Generic Keyboard", composition.Name);            
            Assert.Equal(3, composition.Quantity);            
            Assert.Empty(composition.Composition);             
        }

        private ProductComposition FindProductCompositionById(int id, List<ProductComposition> list)
        {
            return list.Find(e => e.Id == id);
        }

        private List<ProductComposition> GetProductComposition(int id, int level)
        {
            using(var context = new StockHouseContext())
            {
                var productTreeCompositionService = new ProductTreeCompositionService(context);
                return productTreeCompositionService.GetTree(id, level);
            }
        }
    }
}