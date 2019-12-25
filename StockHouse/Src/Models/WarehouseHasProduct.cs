using System.ComponentModel.DataAnnotations.Schema;

namespace StockHouse.Src.Models
{
    /// <summary>
    /// Physical stock representation from item products and warehouses
    /// </summary>
    [Table("warehouse_has_product")]
    public class WarehouseHasProduct
    {
        /// <summary>
        /// Warehouse Id.
        /// </summary>
        /// <value>Warehouse Id</value>
        [Column("warehouse_id")]
        public int WarehouseId { get; set; }
        /// <summary>
        /// Item Product Id.
        /// </summary>
        /// <value>Item Product Id</value>
        [Column("product_id")]
        public int ProductId { get; set; }
        /// <summary>
        /// Physical stock of the Item Product in the warehouse.
        /// </summary>
        /// <value>Physical Stock</value>
        [Column("stock")]
        public int Stock { get; set; }
    }
}