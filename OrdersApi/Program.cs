using Microsoft.EntityFrameworkCore;
using OrdersApi.Commands;
using OrdersApi.Data;
using OrdersApi.Handlers;
using OrdersApi.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BasicDatabaseConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapPost("/api/orders", async (CreateOrderCommand order, AppDbContext db) =>
{
    //order.CreatedAt = DateTime.UtcNow;
    //db.Orders.Add(order);
    //await db.SaveChangesAsync();
    //return Results.Created($"/api/orders/{order.Id}", order);

    var cretedOrderCommand = await CreateOrderCommandHandler.HandleAsync(order, db);
    return Results.Created($"/api/orders/{cretedOrderCommand.Id}", cretedOrderCommand);
});

app.MapGet("/api/orders", async (AppDbContext db) =>
    //await db.Orders.ToListAsync());
    await GetOrdersQueryHandler.HandleAsync(db));

app.MapGet("/api/orders/{id}", async (int id, AppDbContext db) =>
{
    //await db.Orders.FindAsync(id)
    //    is Order order
    //        ? Results.Ok(order)
    //        : Results.NotFound()

    var order = await GetOrderByIdQueryHandler.HandleAsync(new GetOrderByIdQuery(id), db);
    return order is not null
        ? Results.Ok(order)
        : Results.NotFound();
});

app.Run();
