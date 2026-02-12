**Backend:** ASP.NET Core 8 Web API with controllers and a SignalR hub for real-time stock updates.

**API Layer:** Created a .NET Core Web API project Stocks.API containing the StocksController to fetch stock data. Implemented ExceptionMiddleware to handle all unhandled exceptions globally and return user-friendly responses.

**Domain Layer / Services**: Created a separate Domain project Stocks.Domain containing the IStockService interface and its implementation StockService. It includes the StockDetails model to pass data, the StockHub SignalR hub, and the StockBackgroundService to broadcast live stock updates. StockService contains the business logic for fetching stock data and simulates a 10% failure rate. 
This separation keeps controllers thin and improves testability, maintainability, and supports dependency injection.

**Dependency Injection:** IStockService and ILogger are injected into controllers and services to maintain a clean and testable architecture.

**Frontend:** React + Vite + TypeScript for fetching API data and subscribing to SignalR live updates. The frontend project includes:

StockGrid.tsx — a component that renders a live data grid to display stock updates.

Stock.ts — a model defining the stock data structure.

StockApi.ts — a service for fetching stock data from the API, including a retry mechanism for handling API failures.

The frontend subscribes to the SignalR hub to receive live stock updates and updates the grid dynamically. Error scenarios are gracefully handled, displaying user-friendly messages to the user.
