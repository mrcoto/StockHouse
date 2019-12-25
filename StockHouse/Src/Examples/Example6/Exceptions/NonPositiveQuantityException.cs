using System;
namespace StockHouse.Src.Examples.Example6.Exceptions
{
    /// <summary>
    /// Exception thrown when quantity is negative or zero.
    /// </summary>
    public class NonPositiveQuantityException : Exception
    {
        private static readonly String message = "Quantity must be positive";

        /// <summary>
        /// NonPositiveQuantityException Constructor
        /// </summary>
        public NonPositiveQuantityException() : base(message) {}
    }
}