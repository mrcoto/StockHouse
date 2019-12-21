using System.Collections.Generic;
using System;
namespace StockHouse.Src.Examples.Example6.Exceptions
{
    public class NonExistingProductsException : Exception
    {
        public NonExistingProductsException(List<int> productIds) 
                : base($"Following product id's are not present: {String.Join(", ", productIds)}") {}

    }
}