using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockHouse.Src.Models
{
    /// <summary>
    /// Products from StockHouse
    /// </summary>
    [Table("product")]
    public partial class Product
    {
        /// <summary>
        /// This Product class represents, <c>product</c> table from the database.
        /// </summary>
        public Product()
        {
            this.ProductComposition = new List<ProductHasProduct>();
        }

        /// <summary>
        /// Integer identity of the product.
        /// </summary>
        /// <value>product id</value>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// Unique and string identifier of a product.
        /// For example, 'keyboard' representing a 'Generic Keyboard' product.
        /// </summary>
        /// <value>Alias name of a product</value>
        [Column("alias_name")]
        public String AliasName { get; set; }
        /// <summary>
        /// Name or description of the product.
        /// </summary>
        /// <value>Name or description of the product</value>
        [Column("name")]
        public String Name { get; set; }
        /// <summary>
        /// Flag that indicates if the product is an item product or a non-item product.
        /// A non-item product, is a product that is compounded of another products.
        /// And thus, there is no physical stock in warehouses.
        /// </summary>
        /// <value>True: item product, False: non-item product.</value>
        [Column("is_item")]
        public Boolean IsItem { get; set; }
        /// <summary>
        /// Datetime where the product was created.
        /// </summary>
        /// <value>product creation datetime</value>
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Datetime where the product was updated.
        /// </summary>
        /// <value>product update datetime</value>
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Composition of the product.
        /// </summary>
        /// <value>Records from the table 'product_has_product' where 'product_id' is equals
        /// to this product id. </value>
        public List<ProductHasProduct> ProductComposition { get; set; }
        /// <summary>
        /// Products where this product is contained in.
        /// The inverse from <c>ProductComposition</c>
        /// </summary>
        /// <value>Records from the table 'product_has_product' where 'product_content_id' is equals
        /// to this product id. </value>
        public List<ProductHasProduct> ContainedIn { get; set; }
    }
}