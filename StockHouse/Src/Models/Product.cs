using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockHouse.Src.Models
{
    [Table("product")]
    public partial class Product
    {
        public Product()
        {
            this.ProductComposition = new List<ProductHasProduct>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("alias_name")]
        public String AliasName { get; set; }
        [Column("name")]
        public String Name { get; set; }
        [Column("is_item")]
        public Boolean IsItem { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        public List<ProductHasProduct> ProductComposition { get; set; }
        public List<ProductHasProduct> ContainedIn { get; set; }
    }
}