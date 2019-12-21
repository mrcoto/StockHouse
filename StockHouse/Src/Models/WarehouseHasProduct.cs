using System.ComponentModel.DataAnnotations.Schema;

namespace StockHouse.Src.Models
{
    [Table("warehouse_has_product")]
    public class WarehouseHasProduct
    {
        [Column("warehouse_id")]
        public int WarehouseId { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("stock")]
        public int Stock { get; set; }
    }
}