using System.Linq;
using StockHouse.Src.DB;

namespace StockHouse.Src.Examples.Example1
{
    public class GetProductByIdService : StockHouseService
    {
        public GetProductByIdService(StockHouseContext _context) : base(_context) {}

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