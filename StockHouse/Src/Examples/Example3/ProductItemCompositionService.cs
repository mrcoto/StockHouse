using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StockHouse.Src.DB;

namespace StockHouse.Src.Examples.Example3
{
    /// <summary>
    /// Get the item composition of a product. (or leaf nodes in a tree representation).
    /// </summary>
    public class ProductItemCompositionService : StockHouseService
    {
        private static readonly int DEFAULT_FACTOR = 1;

        /// <summary>
        /// ProductItemCompositionService constructor
        /// </summary>
        /// <param name="_context">Database context</param>
        public ProductItemCompositionService(StockHouseContext _context) : base(_context) {}
        
        /// <summary>
        /// Get a flat list item product representation of the contained products.
        /// <para>
        /// The algorithm use Depth-first:
        /// <list>
        /// <item>
        ///     Starting with root product, get all it's children, enqueue non-item product's content id
        ///     in queue and item products in item composition list.
        /// </item>
        /// <item>
        ///     Until, the queue is empty, take one element, get it's children, multiplying by the content factor.
        ///     enqueue non-item product's content id
        ///     in queue and item products in item composition list.
        /// </item>
        /// <item>
        ///     Finally, when queue is empty, all group item composition by the same item and sum quantities.
        /// </item>
        /// </list>
        /// </para>
        /// <para>
        /// For example, given this product composition relationship:
        ///                     A
        ///                   /   \
        ///                 / x3   \ x2 
        ///                B         C
        ///             /x1  \x2    / x4 \ x1
        ///            D   E       E     G
        /// 
        /// Queue: [(A, 1)]
        /// 
        /// Loop 1: 
        ///     Dequeue: (A, 1)
        ///     Queue: [(B, 3), (C, 2)]     Item Composition: []
        /// 
        /// Loop 2: 
        ///     Dequeue: (B, 3)
        ///     Queue: [(C, 2)]     Item Composition: [3xD, 6xE]  | D, E not queued because are item-products
        /// 
        /// Loop 3: 
        ///     Dequeue: (C, 2)
        ///     Queue: []     Item Composition: [3xD, 6xE, 8xE, 2xG]  | E, G not queued because are item-products
        /// 
        /// Group same item and return it:
        ///     Result: Item Composition: [3xD, 14xE, 2xG]
        /// 
        /// </para>
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Tree composition of a product</returns>
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