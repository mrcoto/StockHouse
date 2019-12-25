using System.Linq;
using StockHouse.Src.DB;

namespace StockHouse.Src.Examples.Example1
{
    /// <summary>
    /// Obtain a product from the database, and return its simplified representation.
    /// </summary>
    public class GetProductByIdService : StockHouseService
    {
        /// <summary>
        /// GetProductByIdService constructor
        /// </summary>
        /// <param name="_context">Database context</param>
        public GetProductByIdService(StockHouseContext _context) : base(_context) {}

        /// <summary>
        /// Return simplified representation of a product stored in database.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Simplified product representation</returns>
        public ProductDataDto byId(int id)
        {
            return _context.Products
                           .Where(p => p.Id == id)
                           .Select(p => new ProductDataDto()
                           {
                               AliasName = p.AliasName,
                               Name = p.Name,
                               HasComposition = !p.IsItem
                           })
                           .Single();
        }

    }
}