
namespace StockHouse.Src.DB
{
    /// <summary>
    /// Base service that contains the database context.
    /// </summary>
    public class StockHouseService
    {
        /// <summary>
        ///  Database context.
        /// </summary>
        /// <value>Database context</value>
        protected StockHouseContext _context { get; }

        /// <summary>
        /// StockHouseService constructor
        /// </summary>
        /// <param name="context">Database context</param>
        public StockHouseService(StockHouseContext context)
        {
            this._context = context;
        }
        
    }
}