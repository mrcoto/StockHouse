using System;
using System.Collections.Generic;
using System.Linq;
using StockHouse.Src.DB;
using StockHouse.Src.Examples.Example6;
using StockHouse.Src.Examples.Example6.Exceptions;
using Xunit;

namespace StockHouseTest.Src.Examples.Example6
{

    public class SetProductCompositionServiceTest
    {
        [Fact]
        public void Test_Should_Throw_InvalidOperationException_If_Product_Doesnt_Exists()
        {
            var contents = new List<ProductContent>();
            Assert.Throws<InvalidOperationException>(() => SetProductComposition(productId: -1, contents: contents));
        }

        [Fact]
        public void Test_Should_Throw_NonPositiveQuantityException_If_Quantity_Is_Negative()
        {
            var contents = new List<ProductContent>();
            contents.Add(new ProductContent() { Id = 1, Quantity =  -4 });
            Assert.Throws<NonPositiveQuantityException>(() => SetProductComposition(productId: 4, contents: contents));
        }

        [Fact]
        public void Test_Should_Throw_NonPositiveQuantityException_If_Quantity_Is_Zero()
        {
            var contents = new List<ProductContent>();
            contents.Add(new ProductContent() { Id = 1, Quantity =  0 });
            Assert.Throws<NonPositiveQuantityException>(() => SetProductComposition(productId: 4, contents: contents));
        }

        [Fact]
        public void Test_Should_Throw_NonExistingProductsException()
        {
            var contents = new List<ProductContent>();
            contents.Add(new ProductContent() { Id = 1, Quantity =  2 });
            contents.Add(new ProductContent() { Id = 100, Quantity =  2 });
            contents.Add(new ProductContent() { Id = 200, Quantity =  4 });
            contents.Add(new ProductContent() { Id = 3, Quantity =  4 });
            Assert.Throws<NonExistingProductsException>(() => SetProductComposition(productId: 4, contents: contents));
        }

        [Fact]
        public void Test_Should_Throw_CompositionLoopException_Case_Product_4_In_Product_5()
        {
            var contents = new List<ProductContent>();
            contents.Add(new ProductContent() { Id = 1, Quantity =  2 });
            contents.Add(new ProductContent() { Id = 5, Quantity =  3 });
            contents.Add(new ProductContent() { Id = 2, Quantity =  4 });
            Assert.Throws<CompositionLoopException>(() => SetProductComposition(productId: 4, contents: contents));
        }

        [Fact]
        public void Test_Should_Throw_CompositionLoopException_Case_Product_4_In_Product_8()
        {
            var contents = new List<ProductContent>();
            contents.Add(new ProductContent() { Id = 1, Quantity =  2 });
            contents.Add(new ProductContent() { Id = 8, Quantity =  3 });
            contents.Add(new ProductContent() { Id = 2, Quantity =  4 });
            Assert.Throws<CompositionLoopException>(() => SetProductComposition(productId: 4, contents: contents));
        }

        [Fact]
        public void Test_Should_Set_Composition_Marking_As_Non_Item_Product()
        {
            var contents = new List<ProductContent>();
            contents.Add(new ProductContent() { Id = 1, Quantity =  2 });
            contents.Add(new ProductContent() { Id = 6, Quantity =  3 });
            contents.Add(new ProductContent() { Id = 2, Quantity =  4 });

            SetProductComposition(productId: 4, contents: contents);

            AssertIsItem(productId: 4, isItem: false);
            AssertComposition(productId: 4, productContentId: 1, quantity: 2);
            AssertComposition(productId: 4, productContentId: 6, quantity: 3);
            AssertComposition(productId: 4, productContentId: 2, quantity: 4);
        }

        [Fact]
        public void Test_Should_Throw_CompositionLoopException_Containing_Itself()
        {
            var contents = new List<ProductContent>();
            contents.Add(new ProductContent() { Id = 4, Quantity =  3 });
            contents.Add(new ProductContent() { Id = 2, Quantity =  4 });
            Assert.Throws<CompositionLoopException>(() => SetProductComposition(productId: 4, contents: contents));
        }

        [Fact]
        public void Test_Should_Remove_Composition_Marking_As_Item_Product()
        {
            var contents = new List<ProductContent>();

            SetProductComposition(productId: 4, contents: contents);

            AssertIsItem(productId: 4, isItem: true);
            AssertEmptyComposition(productId: 4);
        }

        private void AssertIsItem(int productId, Boolean isItem)
        {
            using(var context = new StockHouseContext())
            {
                Boolean exists = context.Products
                                        .Any(p => p.Id == productId && p.IsItem == isItem);
                Assert.True(exists);
            }
        }

        private void AssertEmptyComposition(int productId)
        {
            using(var context = new StockHouseContext())
            {
                Boolean exists = context.ProductHasProducts
                                        .Any(php => php.ProductId == productId);
                Assert.False(exists);
            }
        }

        private void AssertComposition(int productId, int productContentId, int quantity)
        {
            using(var context = new StockHouseContext())
            {
                Boolean exists = context.ProductHasProducts
                                        .Any(php => 
                                            php.ProductId == productId && 
                                            php.ProductContentId == productContentId && 
                                            php.Quantity == quantity
                                        );
                Assert.True(exists);
            }
        }

        private void SetProductComposition(int productId, List<ProductContent> contents)
        {
            using(var context = new StockHouseContext())
            {
                var setProductCompositionService = new SetProductCompositionService(context);
                setProductCompositionService.SetComposition(productId, contents);
            }
        }
    }
}