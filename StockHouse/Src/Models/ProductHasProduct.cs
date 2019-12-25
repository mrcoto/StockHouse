using System.ComponentModel.DataAnnotations.Schema;

namespace StockHouse.Src.Models
{
    /// <summary>
    /// Represent the composition table between a product and it's contained products
    /// </summary>
    [Table("product_has_product")]
    public class ProductHasProduct
    {
        /// <summary>
        /// Parent product id or id of the product that contains another products.
        /// </summary>
        /// <value>Parent product id</value>
        [Column("product_id")]
        public int ProductId { get; set; }
        /// <summary>
        /// Parent product or product that contains another products.
        /// </summary>
        /// <value>Parent product</value>
        public Product Product { get; set; }
        /// <summary>
        /// Child product id or id of the product that is contained by another product.
        /// </summary>
        /// <value>Child product id</value>
        [Column("product_content_id")]
        public int ProductContentId { get; set; }
        /// <summary>
        /// Child product or product that is contained by another product.
        /// </summary>
        /// <value>Child product</value>
        public Product ProductContent { get; set; }
        /// <summary>
        /// Quantity of the contained product in another product.
        /// </summary>
        /// <value>Quantity of the contained product in another product</value>
        [Column("quantity")]
        public int Quantity { get; set; }

    }
}