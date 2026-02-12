using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stocks.Domain.Services;

namespace StocksAPI.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly ILogger<StocksController> _logger;

        public StocksController(IStockService stockService, ILogger<StocksController> logger)
        {
            _stockService = stockService;
            _logger = logger;
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
                _logger.LogError(ex, "Failed to retrive stocks info");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
