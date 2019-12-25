using System;

namespace StockHouse.Src.Examples.Example3
{
    /// <summary>
    /// Detailed single row composition of a product.
    /// </summary>
    public class ContentQuantity
    {
        /// <summary>
        /// Parent product id or product that contains another products.
        /// </summary>
        /// <value>parent id</value>
        public int ParentId { get; set; }
        /// <summary>
        /// Contained product id
        /// </summary>
        /// <value>id of the contained product</value>
        public int ProductContentId { get; set; }
        /// <summary>
        /// Contained product alias name
        /// </summary>
        /// <value>Alias name of the contained product</value>
        public String AliasName { get; set; }
        /// <summary>
        /// Contained product name
        /// </summary>
        /// <value>Name or description of the contained product</value>
        public String Name { get; set; }
        /// <summary>
        /// Flag that indicates if the product is an item product or a non-item product.
        /// A non-item product, is a product that is compounded of another products.
        /// And thus, there is no physical stock in warehouses.
        /// </summary>
        /// <value>True: item product, False: non-item product.</value>
        public Boolean IsItem { get; set; }
        /// <summary>
        /// Quantity of the contained product.
        /// </summary>
        /// <value>Quantity of the contained product</value>
        public int Quantity { get; set; }
    }
}