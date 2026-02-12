using Stocks.Domain.Services;
using StockApp.Api.Services;
using Stocks.Domain.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddSingleton<IStockService, StockService>();
builder.Services.AddHostedService<StockBackgroundService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("StocksCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); 
    });
});

builder.Services.AddControllers();

var app = builder.Build();
app.UseCors("StocksCorsPolicy");

app.UseAuthorization();

app.MapControllers();
app.MapHub<StockHub>("/stockhub");

app.Run();
