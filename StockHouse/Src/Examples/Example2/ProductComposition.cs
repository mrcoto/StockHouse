using System.Collections.Generic;
using System;

namespace StockHouse.Src.Examples.Example2
{
    public class ProductComposition
    {
        public ProductComposition()
        {
            this.Composition = new List<ProductComposition>();
        }

        public int Id { get; set; }
        public int ParentId { get; set; }
        public String AliasName { get; set; }
        public String Name { get; set; }
        public int Quantity { get; set; }
        public List<ProductComposition> Composition { get; set; }

        public override String ToString()
        {
            return $"ProductComposition(Id = {Id}, ParentId = {AliasName}, Name = {Name}, Quantity = {Quantity})";
        }

    }
}