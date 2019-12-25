using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockHouse.Src.Models
{
    /// <summary>
    /// StockHouse Warehouses
    /// </summary>
    [Table("warehouse")]
    public class Warehouse
    {
        /// <summary>
        /// Warehouse identifier.
        /// </summary>
        /// <value>Warehouse Id</value>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// StockHouse warehouse string identifier. For example "Warehouse A" or simply "A".
        /// </summary>
        /// <value>Name or string identifier</value>
        [Column("name")]
        public String Name { get; set; }
        /// <summary>
        /// Datetime where the warehouse was created.
        /// </summary>
        /// <value>warehouse creation datetime</value>
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Datetime where the warehouse was updated.
        /// </summary>
        /// <value>warehouse update datetime</value>
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}