using System;
using StockHouse.Src.DB;
using StockHouse.Src.Examples.Example2;

namespace StockHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var context = new StockHouseContext())
            {
                var treeCompositionService = new ProductTreeCompositionService(context);
                var response = treeCompositionService.GetTree(7, 2);
                response.ForEach(c => 
                {
                    Console.WriteLine(c);
                    c.Composition.ForEach(c => Console.WriteLine(" > " + c));
                });
            }
        }

    }
}
