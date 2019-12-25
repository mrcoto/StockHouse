using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StockHouse.Src.DB;

namespace StockHouse.Src.Examples.Example2
{
    /// <summary>
    /// Return a product composition as a tree.
    /// </summary>
    public class ProductTreeCompositionService : StockHouseService
    {
        /// <summary>
        /// ProductTreeCompositionService constructor
        /// </summary>
        /// <param name="_context">Database context</param>
        public ProductTreeCompositionService(StockHouseContext _context) : base(_context) {}
        
        /// <summary>
        /// Get a tree representation of a product composition.
        /// <para>
        /// The algorithm is:
        /// <list>
        /// <item>
        ///     Starting with root product, get all it's children and put them in a temporal list and a permanent list.
        /// </item>
        /// <item>
        ///     Take all children from the temporal list, and get all their childrens, remove the elements
        ///     from temporal list, and put the childrens in the temporal list and permanent list. Repeat until
        ///     the desired level, or when the another list is empty (no more children to retrieve).
        /// </item>
        /// </list>
        /// </para>
        /// <para>
        /// For example, given this product composition relationship:
        ///                   A
        ///                /    \
        ///               B     C
        ///             /  \   / \ 
        ///            D   E  F   G
        /// 
        /// Starting with A, it's children are 'B' and 'C'.
        /// Loop 1:
        ///     Temporal List: ['B', 'C'], Permanent List: ['B', 'C']. (If level is 1, then stop here)
        /// Loop 2:
        ///     Temporal list: ['D', 'E', 'F', 'G'], Permanent List: ['B', 'C', 'D', 'E', 'F', 'G']. (If level is 2, then stop here)
        /// Loop 3:
        ///     Temporal List: []. Permanent List: ['B', 'C', D', 'E', 'F', 'G']. No more items, stop here
        /// </para>
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="level">Level of the tree</param>
        /// <returns>Tree composition of a product</returns>
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

        /// <summary>
        /// Transform the plain list to tree representation
        /// <para>
        /// The algorithm is:
        /// <list>
        /// <item>
        ///     Generate a copy of the list (permanent list) and reverse the permanent list 
        ///     (with this way, all children will be put at the end, starting with leaf nodes).
        /// </item>
        /// <item>
        ///     From each element of permanent list, look up its parent. If parent not exists, then is a root element.
        ///     Otherwise, remove it from copy, and put it in the parent composition list.
        /// </item>
        /// </list>
        /// </para>
        /// <para>
        /// For example, From the previous example:
        ///     Permanent List: ['B', 'C', D', 'E', 'F', 'G'] -> reversed: ['G', 'F', 'E', 'D', 'C', 'B'].
        /// 
        /// Loop 1:
        ///     'G' is children of 'C', then remove it from the copy and put in the composition List of 'C'.
        ///     Copy List ['B', 'C' -> ['G'], 'D', 'E', 'F'].
        /// 
        /// Loop 2:
        ///     'F' is children of 'C', then remove it from the copy and put in the composition List of 'C'.
        ///     Copy List ['B', 'C' -> ['G', 'F'], 'D', 'E'].
        /// 
        /// Loop 3:
        ///     'E' is children of 'B', then remove it from the copy and put in the composition List of 'B'.
        ///     Copy List ['B' -> ['E'], 'C' -> ['G', 'F'], 'D'].
        /// 
        /// Loop 4:
        ///     'D' is children of 'B', then remove it from the copy and put in the composition List of 'B'.
        ///     Copy List ['B' -> ['E', 'D'], 'C' -> ['G', 'F']].
        /// 
        /// Loop 5:
        ///     'C' has no parent, then do nothing.
        ///     Copy List ['B' -> ['E', 'D'], 'C' -> ['G', 'F']].
        /// 
        /// Loop 6:
        ///     'B' has no parent, then do nothing.
        ///     Copy List ['B' -> ['E', 'D'], 'C' -> ['G', 'F']].
        /// </para>
        /// </summary>
        /// <param name="compositions">Permanent list of the product composition</param>
        /// <returns>Tree list representation</returns>
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