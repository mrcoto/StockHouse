using System.Collections.Generic;
using System;

namespace StockHouse.Src.Examples.Example2
{
    /// <summary>
    /// Detailed single row composition of a product.
    /// </summary>
    public class ProductComposition
    {
        /// <summary>
        /// Detailed single row composition of a product.
        /// </summary>
        public ProductComposition()
        {
            this.Composition = new List<ProductComposition>();
        }
        /// <summary>
        /// Integer identity of the product.
        /// </summary>
        /// <value>product id</value>
        public int Id { get; set; }
        /// <summary>
        /// Parent product id or product that contains another products.
        /// </summary>
        /// <value>parent id</value>
        public int ParentId { get; set; }
        /// <summary>
        /// Contained product alias name
        /// </summary>
        /// <value>Alias name of a contained product</value>
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
        /// Composition or contained products by the contained product.
        /// For example, with this relationship: A -> B -> C.
        /// 
        /// If, ParentId is A's id, then AliasName and Name are attributes from "B", Quantity
        /// indicates how many "B" are in "A". Also, Composition should contain a list from this same object,
        /// with ParentId equals to B's id, Alias Name and Name from "C", and Composition of the C object.
        /// </summary>
        /// <value></value>
        public List<ProductComposition> Composition { get; set; }

        /// <summary>
        /// String representation
        /// </summary>
        /// <returns>String representation of the object</returns>
        public override String ToString()
        {
            return $"ProductComposition(Id = {Id}, AliasName = {AliasName}, Name = {Name}, Quantity = {Quantity})";
        }

    }
}