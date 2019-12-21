using System;

namespace StockHouse.Src.Examples.Example4
{
    public class ProductInfo
    {
        public int Id { get; set; }
        public String AliasName { get; set; }
        public String Name { get; set; }
        public Boolean IsItem { get; set; }

        public override String ToString()
        {
            return $"ProductInfo(Id = {Id}, AliasName = {AliasName}, Name = {Name}, IsItem = {IsItem})";
        }
    }
}