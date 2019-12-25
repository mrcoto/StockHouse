using System.Collections.Generic;
using System.Linq;
using StockHouse.Src.DB;
using StockHouse.Src.Examples.Example3;
using StockHouse.Src.Examples.Example4;
using StockHouse.Src.Models;

namespace StockHouse.Src.Examples.Example5
{
    /// <summary>
    /// Return the logical or physical stock of a product in a specific warehouse.
    /// </summary>
    public class ProductLogicalStockService : StockHouseService
    {

        private ProductItemCompositionService productItemCompositionService;

        /// <summary>
        /// ProductLogicalStockService constructor.
        /// </summary>
        /// <param name="_context">Database context</param>
        /// <returns></returns>
        public ProductLogicalStockService(StockHouseContext _context) : base(_context) 
        {
            this.productItemCompositionService = new ProductItemCompositionService(_context);
        }

        /// <summary>
        /// Get the physicial stock if product is item, or the logical stock if product is non-item.
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="warehouseId">Warehouse id</param>
        /// <returns>Logical or Physical stock</returns>
        public int GetStock(int productId, int warehouseId)
        {
            var product = GetProduct(productId);
            CheckWarehouse(warehouseId);
            if (product.IsItem)
            {
                return GetPhysicalStock(productId, warehouseId);
            }
            var itemCompositions = productItemCompositionService.GetItems(productId);
            return GetLogicalStock(itemCompositions, warehouseId);
        }

        private int GetPhysicalStock(int productId, int warehouseId)
        {
            return _context.WarehouseHasProducts
                           .Where(whp => whp.ProductId == productId && whp.WarehouseId == warehouseId)
                           .SingleOrDefault()?.Stock ?? 0;
        }

        private int GetLogicalStock(List<ItemComposition> itemCompositions, int warehouseId)
        {
            var productIds = itemCompositions.Select(ic => ic.Id).ToList();
            var productStocks = GetProductStocks(productIds, warehouseId);
            var stocks = itemCompositions.Select(ic => 
            {
                var stock = productStocks.Where(ps => ps.ProductId == ic.Id).SingleOrDefault()?.Stock ?? 0;
                return stock / ic.Quantity; // int division. with (double) is double division
            }).ToList();
            return stocks.Any() ? stocks.Min() : 0;
        }

        private List<WarehouseHasProduct> GetProductStocks(List<int> productIds, int warehouseId)
        {
            return _context.WarehouseHasProducts
                           .Where(whp => productIds.Contains(whp.ProductId) && whp.WarehouseId == warehouseId)
                           .ToList();
        }

        private ProductInfo GetProduct(int id)
        {
            return _context.Products
                           .Where(p => p.Id == id)
                           .Select(p => new ProductInfo()
                           {
                               Id = p.Id,
                               AliasName = p.AliasName,
                               Name = p.Name,
                               IsItem = p.IsItem
                           })
                           .Single();
        }

        private void CheckWarehouse(int id)
        {
            _context.Warehouses
                    .Where(w => w.Id == id)
                    .Single();
        }

    }
}