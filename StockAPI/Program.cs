using Stock.Domain.Services;
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
              .AllowCredentials(); // required for SignalR
    });
});
// Add services to the container.
builder.Services.AddControllers();


var app = builder.Build();
app.UseCors("StocksCorsPolicy");

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();
app.MapHub<StockHub>("/stockhub");

app.Run();
