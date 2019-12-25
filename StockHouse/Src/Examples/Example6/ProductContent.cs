using System;

namespace StockHouse.Src.Examples.Example6
{
    /// <summary>
    /// Represents a product content from another product
    /// </summary>
    public class ProductContent
    {
        /// <summary>
        /// The contained product id.
        /// </summary>
        /// <value>Product id</value>
        public int Id { get; set; }
        /// <summary>
        /// Quantity of the contained product.
        /// </summary>
        /// <value>Quantity of the contained product</value>
        public int Quantity { get; set; }

        /// <summary>
        /// String representation
        /// </summary>
        /// <returns>String representation of the object</returns>
        public override String ToString()
        {
            return $"ProductContent(Id = {Id}, Quantity = {Quantity})";
        }
    }
}