using System.Collections.Generic;
using System.Linq;
using StockHouse.Src.DB;
using StockHouse.Src.Examples.Example6.Exceptions;
using StockHouse.Src.Models;

namespace StockHouse.Src.Examples.Example6
{
    public class SetProductCompositionService : StockHouseService
    {
        public SetProductCompositionService(StockHouseContext _context) : base(_context) { }

        public void SetComposition(int id, List<ProductContent> contents)
        {
            var product = GetProduct(id);
            CheckQuantities(contents);
            CheckProductContents(contents);
            RemoveOldComposition(id);
            CheckCompositionLoop(id, contents);
            var isItem = !contents.Any();
            if (!isItem)
            {
                SetNewComposition(product, contents);
            }
            product.IsItem = isItem;
            _context.Attach(product);
            _context.SaveChanges();
        }

        private Product GetProduct(int id)
        {
            return _context.Products
                           .Where(p => p.Id == id)
                           .Single();
        }

        private void CheckQuantities(List<ProductContent> contents)
        {
            var throwException = contents.Any(c => c.Quantity <= 0);
            if (throwException)
            {
                throw new NonPositiveQuantityException();
            }
        }
    
        private void CheckProductContents(List<ProductContent> contents)
        {
            var productIds = contents.Select(c => c.Id).ToList();
            var foundProductIds = GetFoundProductIds(productIds);
            var diff = productIds.Except(foundProductIds).ToList();
            if (diff.Any())
            {
                throw new NonExistingProductsException(diff);
            }
        }

        private List<int> GetFoundProductIds(List<int> productIds)
        {
            return _context.Products
                           .Where(p => productIds.Contains(p.Id))
                           .Select(p => p.Id)
                           .ToList();
        }
    
        private void RemoveOldComposition(int productId)
        {
            _context.ProductHasProducts.RemoveRange(
                _context.ProductHasProducts.Where(php => php.ProductId == productId)
            );
        }

        private void CheckCompositionLoop(int productId, List<ProductContent> contents)
        {
            var productIds = contents.Select(c => c.Id).ToList();
            while(productIds.Any())
            {
                if (productIds.Contains(productId))
                {
                    throw new CompositionLoopException();
                }
                productIds = GetProductContentIds(productIds);
            }
        }

        private List<int> GetProductContentIds(List<int> productParentIds)
        {
            return _context.ProductHasProducts
                           .Where(php => productParentIds.Contains(php.ProductId))
                           .Select(php => php.ProductContentId)
                           .ToList();
        }

        private void SetNewComposition(Product product, List<ProductContent> contents)
        {
            product.ProductComposition.AddRange(
                contents.Select(c => new ProductHasProduct()
                {
                    ProductId = product.Id,
                    ProductContentId = c.Id,
                    Quantity = c.Quantity
                })
            );
        }

    }
}