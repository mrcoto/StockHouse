using System;
using StockHouse.Src.DB;
using StockHouse.Src.Examples.Example4;

namespace StockHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var context = new StockHouseContext())
            {
                var getProductsBySearchTokenService = new GetProductsBySearchTokenService(context);
                var response = getProductsBySearchTokenService.Search("game");
                response.ForEach(c => Console.WriteLine(c));
            }
        }

    }
}
