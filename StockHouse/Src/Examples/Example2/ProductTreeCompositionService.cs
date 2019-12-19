using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StockHouse.Src.DB;

namespace StockHouse.Src.Examples.Example2
{
    public class ProductTreeCompositionService : StockHouseService
    {
        public ProductTreeCompositionService(StockHouseContext _context) : base(_context) {}
        
        public List<ProductComposition> GetTree(int id, int level)
        {
            if (level <= 0)
            {
                throw new ArgumentException($"Level '{level}' is not greater or equals than 1");
            }
            return GetTreeComposition(id, level);
        }

        private List<ProductComposition> GetTreeComposition(int productId, int maxLevel)
        {
            var compositions = new List<ProductComposition>();
            var list = new List<int>();
            list.Add(productId);
            var level = 1;
            while(list.Count > 0 && level <= maxLevel)
            {
                var productCompositions = GetProductCompositions(list);
                list.RemoveAll(x => true);
                list.AddRange(productCompositions.Select(pc => pc.Id));
                compositions.AddRange(productCompositions);
                level++;
            }
            return BuildTree(compositions);
        }

        private List<ProductComposition> GetProductCompositions(List<int> productIds)
        {
            return _context.ProductHasProducts
                           .Where(php => productIds.Contains(php.ProductId))
                           .Include(php => php.ProductContent)
                           .Select(php => new ProductComposition()
                           {
                               ParentId = php.ProductId,
                               Id = php.ProductContentId,
                               AliasName = php.ProductContent.AliasName,
                               Name = php.ProductContent.Name,
                               Quantity = php.Quantity
                           })
                           .ToList();
        }

        private List<ProductComposition> BuildTree(List<ProductComposition> compositions)
        {
            var compositionsCopy = compositions.Select(c => c).ToList();
            compositions.Reverse();
            compositions.ForEach(c => 
            {
                var parent = compositionsCopy.FirstOrDefault(comp => comp.Id == c.ParentId);
                if (parent != null)
                {
                    parent.Composition.Add(c);
                    compositionsCopy.RemoveAll(comp => comp.Id == c.Id);
                }
            });
            return compositionsCopy;
        }

    }
}