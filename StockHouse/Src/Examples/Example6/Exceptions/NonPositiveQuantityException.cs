using System;
namespace StockHouse.Src.Examples.Example6.Exceptions
{
    public class NonPositiveQuantityException : Exception
    {
        private static readonly String message = "Quantity must be positive";
        
        public NonPositiveQuantityException() : base(message) {}
    }
}