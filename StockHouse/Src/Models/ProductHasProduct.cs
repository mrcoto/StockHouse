using System.ComponentModel.DataAnnotations.Schema;

namespace StockHouse.Src.Models
{
    [Table("product_has_product")]
    public class ProductHasProduct
    {
        [Column("product_id")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Column("product_content_id")]
        public int ProductContentId { get; set; }
        public Product ProductContent { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }

    }
}