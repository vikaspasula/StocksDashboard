using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock.Domain.Services;

namespace StockAPI.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var stocks = await _stockService.GetStocksAsync();

                return Ok(stocks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
