using System.Collections.Generic;
using System;
namespace StockHouse.Src.Examples.Example6.Exceptions
{
    /// <summary>
    /// Exception thrown when one or more product's id doesn't exists in database
    /// </summary>
    public class NonExistingProductsException : Exception
    {
        /// <summary>
        /// NonExistingProductsException Constructor.
        /// </summary>
        /// <param name="productIds">List of product's id.</param>
        public NonExistingProductsException(List<int> productIds) 
                : base($"Following product id's are not present: {String.Join(", ", productIds)}") {}

    }
}