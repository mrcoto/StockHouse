using System;

namespace StockHouse.Src.Examples.Example4
{
    /// <summary>
    /// Subset of Product entity
    /// </summary>
    public class ProductInfo
    {
        /// <summary>
        /// Integer identity of the product.
        /// </summary>
        /// <value>product id</value>
        public int Id { get; set; }
        /// <summary>
        /// Unique and string identifier of a product.
        /// For example, 'keyboard' representing a 'Generic Keyboard' product.
        /// </summary>
        /// <value>Alias name of a product</value>
        public String AliasName { get; set; }
        /// <summary>
        /// Name or description of the product.
        /// </summary>
        /// <value>Name or description of the product</value>
        public String Name { get; set; }
        /// <summary>
        /// Flag that indicates if the product is an item product or a non-item product.
        /// A non-item product, is a product that is compounded of another products.
        /// And thus, there is no physical stock in warehouses.
        /// </summary>
        /// <value>True: item product, False: non-item product.</value>
        public Boolean IsItem { get; set; }
        /// <summary>
        /// String representation
        /// </summary>
        /// <returns>String representation of the object</returns>
        public override String ToString()
        {
            return $"ProductInfo(Id = {Id}, AliasName = {AliasName}, Name = {Name}, IsItem = {IsItem})";
        }
    }
}