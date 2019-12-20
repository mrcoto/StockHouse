using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StockHouse.Src.DB;

namespace StockHouse.Src.Examples.Example3
{
    public class ProductItemCompositionService : StockHouseService
    {
        private static readonly int DEFAULT_FACTOR = 1;

        public ProductItemCompositionService(StockHouseContext _context) : base(_context) {}
        
        public List<ItemComposition> GetItems(int id)
        {
            var compositions = new List<ContentQuantity>();
            var parentContentQuantities = new List<ContentQuantity>();
            var list = new List<int>();
            list.Add(id);
            while(list.Count > 0)
            {
                var contentQuantities = GetContentQuantities(list);

                var itemCompositions = contentQuantities.Where(cq => cq.IsItem).ToList();
                itemCompositions = MultiplyQuantities(parentContentQuantities, itemCompositions);
                compositions.AddRange(itemCompositions);

                list.RemoveAll(x => true);
                list.AddRange(contentQuantities.Select(cq => cq.ProductContentId));

                parentContentQuantities = contentQuantities.Select(cq => cq).ToList();
            }
            return BuildItemListComposition(compositions);
        }

        private List<ContentQuantity> GetContentQuantities(List<int> productIds)
        {
            return _context.ProductHasProducts
                           .Where(php => productIds.Contains(php.ProductId))
                           .Include(php => php.ProductContent)
                           .Select(php => new ContentQuantity()
                           {
                                ParentId = php.ProductId,
                                ProductContentId = php.ProductContentId,
                                AliasName = php.ProductContent.AliasName,
                                Name = php.ProductContent.Name,
                                IsItem = php.ProductContent.IsItem,
                                Quantity = php.Quantity
                           })
                           .ToList();
        }

        private List<ContentQuantity> MultiplyQuantities(List<ContentQuantity> parentContentQuantities, List<ContentQuantity> itemCompositions)
        {
            return itemCompositions.Select(ic => 
            {
                ic.Quantity *= GetMultiplierFactor(parentContentQuantities, ic.ParentId);
                return ic;
            }).ToList();
        }

        private int GetMultiplierFactor(List<ContentQuantity> contentQuantities, int productContentId)
        {
            var quantities = contentQuantities.Where(cq => cq.ProductContentId == productContentId)
                                              .Select(cq => cq.Quantity)
                                              .ToList();
            if (quantities.Count == 0)
            {
                return DEFAULT_FACTOR;
            }
            return quantities.Aggregate((quantity1, quantity2) => quantity1 * quantity2);
        }

        private List<ItemComposition> BuildItemListComposition(List<ContentQuantity> composition)
        {
            return composition.GroupBy(cq => cq.ProductContentId)
                       .Select(g => new ItemComposition()
                       {    
                           Id = g.Select(cq => cq.ProductContentId).FirstOrDefault(),
                           AliasName = g.Select(cq => cq.AliasName).FirstOrDefault(),
                           Name = g.Select(cq => cq.Name).FirstOrDefault(),
                           Quantity = g.Sum(cq => cq.Quantity)
                       })
                       .ToList();
        }

    }
}