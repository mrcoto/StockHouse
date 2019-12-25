using System;

namespace StockHouse.Src.Examples.Example3
{
    /// <summary>
    /// Detailed single leaf node composition of a product.
    /// </summary>
    public class ItemComposition
    {
        /// <summary>
        /// ID of the contained item product
        /// </summary>
        /// <value>ID of the contained item product</value>
        public int Id { get; set; }
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
            return $"ItemComposition(Id = {Id}, AliasName = {AliasName}, Name = {Name}, Quantity = {Quantity})";
        }
    }
}