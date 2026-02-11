using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Models;

namespace Stock.Domain.Services
{
    public interface IStockService
    {
        Task<List<StockDetails>> GetStocksAsync();
    }
}
