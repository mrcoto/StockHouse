using System;

namespace StockHouse.Src.Examples.Example3
{
    public class ContentQuantity
    {
        public int ParentId { get; set; }
        public int ProductContentId { get; set; }
        public String AliasName { get; set; }
        public String Name { get; set; }
        public Boolean IsItem { get; set; }
        public int Quantity { get; set; }
    }
}