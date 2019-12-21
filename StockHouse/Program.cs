using System;
using System.Collections.Generic;
using StockHouse.Src.DB;
using StockHouse.Src.Examples.Example6;

namespace StockHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var context = new StockHouseContext())
            {
                var contents = new List<ProductContent>();
                contents.Add(new ProductContent() { Id = 1, Quantity =  2 });
                contents.Add(new ProductContent() { Id = 6, Quantity =  3 });
                contents.Add(new ProductContent() { Id = 2, Quantity =  4 });
                var setProductCompositionService = new SetProductCompositionService(context);
                setProductCompositionService.SetComposition(4, contents);
            }
        }

    }
}
