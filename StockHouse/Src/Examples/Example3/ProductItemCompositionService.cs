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
            var queue = new Queue<(int, int)>();
            queue.Enqueue((id, DEFAULT_FACTOR));
            while(queue.Count > 0)
            {
                var (productId, quantityFactor) = queue.Dequeue();
                var contentQuantities = GetContentQuantities(productId, quantityFactor);
                
                contentQuantities.Where(cq => !cq.IsItem)
                                 .ToList()
                                 .ForEach(pcq => queue.Enqueue((pcq.ProductContentId, pcq.Quantity)));

                var itemCompositions = contentQuantities.Where(cq => cq.IsItem).ToList();
                compositions.AddRange(itemCompositions);

            }
            return BuildItemListComposition(compositions);
        }

        private List<ContentQuantity> GetContentQuantities(int productId, int quantityFactor)
        {
            return _context.ProductHasProducts
                            .Where(php => php.ProductId == productId)
                            .Include(php => php.ProductContent)
                            .Select(php => new ContentQuantity()
                            {
                                    ParentId = php.ProductId,
                                    ProductContentId = php.ProductContentId,
                                    AliasName = php.ProductContent.AliasName,
                                    Name = php.ProductContent.Name,
                                    IsItem = php.ProductContent.IsItem,
                                    Quantity = php.Quantity * quantityFactor
                            })
                            .ToList();
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