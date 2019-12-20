using System;

namespace StockHouse.Src.Examples.Example3
{
    public class ItemComposition
    {
        
        public int Id { get; set; }
        public String AliasName { get; set; }
        public String Name { get; set; }
        public int Quantity { get; set; }

        public override String ToString()
        {
            return $"ItemComposition(Id = {Id}, AliasName = {AliasName}, Name = {Name}, Quantity = {Quantity})";
        }
    }
}