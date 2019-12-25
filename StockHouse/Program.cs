using System;
using StockHouse.Src.DB;
using StockHouse.Src.Examples.Example3;

namespace StockHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var context = new StockHouseContext())
            {
                var productItemCompositionService = new ProductItemCompositionService(context);
                var itemComposition = productItemCompositionService.GetItems(9);
                itemComposition.ForEach(ic => Console.WriteLine(ic));
            }
        }

    }
}
