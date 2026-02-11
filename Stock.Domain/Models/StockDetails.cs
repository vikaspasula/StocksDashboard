using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Models
{
    public class StockDetails
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
