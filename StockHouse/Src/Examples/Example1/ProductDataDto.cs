using System;

namespace StockHouse.Src.Examples.Example1
{
    /// <summary>
    /// Subset of product entity.
    /// Alias name, name and if the product has composition.
    /// </summary>
    public class ProductDataDto
    {
        /// <summary>
        /// Unique and string identifier of a product.
        /// For example, 'keyboard' representing a 'Generic Keyboard' product.
        /// </summary>
        /// <value>Alias name of a product</value>
        public String AliasName{ get; set; }
        /// <summary>
        /// Name or description of the product.
        /// </summary>
        /// <value>Name or description of the product</value>
        public String Name { get; set; }
        /// <summary>
        /// Flag that indicates if the product contains another products.
        /// </summary>
        /// <value>True: has composition, otherwise False.</value>
        public Boolean HasComposition { get; set; }

    }
}