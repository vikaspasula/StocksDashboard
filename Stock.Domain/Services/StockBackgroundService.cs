using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Stocks.Domain.Models;
using Stocks.Domain.Hubs;

namespace StockApp.Api.Services;

public class StockBackgroundService : BackgroundService
{
    private readonly IHubContext<StockHub> hubContext;

    private readonly Random random = new();

    public StockBackgroundService(
        IHubContext<StockHub> hubContext)
    {
        this.hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var stock = new StockDetails
            {
                Symbol = "Stock1",
                Price = random.Next(100, 200),
                LastUpdated = DateTime.UtcNow
            };

            await hubContext.Clients.All.SendAsync(
                "ReceiveStockUpdate",
                stock,
                stoppingToken);

            await Task.Delay(2000, stoppingToken);
        }
    }
}
