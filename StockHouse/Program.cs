using System;
using StockHouse.Src.DB;
using StockHouse.Src.Examples.Example2;
using StockHouse.Src.Examples.Example3;

namespace StockHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var context = new StockHouseContext())
            {
                var itemCompositionService = new ProductItemCompositionService(context);
                var response = itemCompositionService.GetItems(8);
                response.ForEach(c => Console.WriteLine(c));
            }
        }

    }
}
