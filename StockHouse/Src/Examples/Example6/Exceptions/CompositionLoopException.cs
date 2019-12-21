using System;
namespace StockHouse.Src.Examples.Example6.Exceptions
{
    public class CompositionLoopException : Exception
    {

        private static readonly String message = "Product content generates a composition loop";

        public CompositionLoopException() : base(message) { }
    }
}