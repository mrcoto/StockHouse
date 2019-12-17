
namespace StockHouse.Src.DB
{
    public class StockHouseService
    {
        protected StockHouseContext _context { get; }

        public StockHouseService(StockHouseContext context)
        {
            this._context = context;
        }
        
    }
}