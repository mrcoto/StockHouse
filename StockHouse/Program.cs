using System;
using StockHouse.Src;
using StockHouse.Src.DB;
using StockHouse.Src.Examples.Example1;

namespace StockHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var context = new StockHouseContext())
            {
                var getProductByIdService = new GetProductByIdService(context);
                var response = getProductByIdService.byId(1);
                Console.WriteLine(response.AliasName);
                Console.WriteLine(response.Name);
                Console.WriteLine(response.HasComposition);
            }
        }
    }
}
