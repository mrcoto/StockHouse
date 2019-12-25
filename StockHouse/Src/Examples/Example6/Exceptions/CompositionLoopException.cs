using System;
namespace StockHouse.Src.Examples.Example6.Exceptions
{
    /// <summary>
    /// Exception thrown when a product causes a Composition Loop.
    /// For example, If product A contains product B, and product B contains product C, then
    /// product C can't contains product A, or product C can't contains product B,
    /// or product C can't contains product C.
    /// </summary>
    public class CompositionLoopException : Exception
    {

        private static readonly String message = "Product content generates a composition loop";

        /// <summary>
        /// Composition Loop Constructor
        /// </summary>
        public CompositionLoopException() : base(message) { }
    }
}