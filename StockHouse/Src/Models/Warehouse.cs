using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockHouse.Src.Models
{
    [Table("warehouse")]
    public class Warehouse
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public String Name { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}