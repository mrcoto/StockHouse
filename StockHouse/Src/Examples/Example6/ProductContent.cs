using System;

namespace StockHouse.Src.Examples.Example6
{
    public class ProductContent
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        
        public override String ToString()
        {
            return $"ProductContent(Id = {Id}, Quantity = {Quantity})";
        }
    }
}