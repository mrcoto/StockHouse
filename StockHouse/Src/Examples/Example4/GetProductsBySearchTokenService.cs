using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StockHouse.Src.DB;

namespace StockHouse.Src.Examples.Example4
{
    public class GetProductsBySearchTokenService : StockHouseService
    {
        public GetProductsBySearchTokenService(StockHouseContext _context) : base(_context) {}

        public List<ProductInfo> Search(String searchToken)
        {
            var query = _context.Products.Select(p => new ProductInfo()
                                            {
                                                Id = p.Id,
                                                AliasName = p.AliasName,
                                                Name = p.Name,
                                                IsItem = p.IsItem
                                            });
            if (!String.IsNullOrEmpty(searchToken)) 
            {
                query = GetWhereClause(query, searchToken);
            }
            return query.ToList();
        }

        private IQueryable<ProductInfo> GetWhereClause(IQueryable<ProductInfo> query, string searchToken)
        {
            searchToken = $"%{searchToken.ToLower()}%";
            return query.Where(p => 
                            EF.Functions.Like(p.AliasName.ToLower(), searchToken) || 
                            EF.Functions.Like(p.Name.ToLower(), searchToken)
                        );
        }

    }
}