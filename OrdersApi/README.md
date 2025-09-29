# OrdersApi

A minimal .NET 9 Web API for managing orders, demonstrating CQRS, MediatR, FluentValidation, and Entity Framework Core with separate read/write databases.

## Features

- **Create Order:** `POST /api/orders`  
  Validates and creates a new order.
- **Get All Orders:** `GET /api/orders`  
  Retrieves all orders.
- **Get Order by ID:** `GET /api/orders/{id}`  
  Fetches details for a specific order.

## Technologies

- .NET 9 / C# 13
- Entity Framework Core (SQLite)
- CQRS Pattern (separate read/write DbContexts)
- MediatR (request/response and notification handling)
- FluentValidation (input validation)
- OpenAPI/Swagger (API documentation in development)

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQLite](https://www.sqlite.org/download.html)

### Setup

1. **Clone the repository:**
    ```sh
    git clone <repository-url>
    cd OrdersApi
    ```

2. **Configure connection strings:**  
   Edit `appsettings.json` for `ReadDatabaseConnection` and `WriteDatabaseConnection`.

3. **Restore dependencies:**
    ```sh
    dotnet restore
    ```

4. **Run database migrations (if applicable):**
    ```sh
    dotnet ef database update --context ReadAppDbContext
    dotnet ef database update --context WriteAppDbContext
    ```

5. **Start the API:**
    ```sh
    dotnet run
    ```

6. **Access Swagger/OpenAPI docs:**  
   Visit `http://localhost:<port>/swagger` (in development mode).

## Usage

### Create an Order

POST /api/orders Content-Type: application/json
{ "customerName": "John Doe" }


### Get All Orders

GET /api/orders

### Get Order by ID

GET /api/orders/{id}


## Project Structure

- `Commands/` – Command objects and validators
- `Queries/` – Query objects
- `Handlers/` – MediatR handlers
- `Events/` – Domain events
- `Projections/` – Event projection handlers
- `Data/` – Entity Framework DbContexts
- `DTOs/` – Data transfer objects

## License

MIT License