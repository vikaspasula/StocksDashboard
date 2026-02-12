using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocks.Domain.Models;
using Stocks.Domain.Services;

namespace Stocks.Domain.Services
{
    public class StockService : IStockService
    {

        private readonly Random random = new();

        public async Task<List<StockDetails>> GetStocksAsync()
        {
            // simulate async operation (like DB/API call)
            await Task.Delay(50);

            // simulate 10% failure
            if (random.Next(1, 11) == 1)
            {
                throw new Exception("Simulated random failure");
            }

            var stocks = new List<StockDetails>
        {
            new StockDetails
            {
                Symbol = "Stock1",
                Price = random.Next(100,200),
                LastUpdated = DateTime.UtcNow
            },
            new StockDetails
            {
                Symbol = "Stock2",
                Price = random.Next(200,400),
                LastUpdated = DateTime.UtcNow
            }
        };

            return stocks;
        }
    }
}
