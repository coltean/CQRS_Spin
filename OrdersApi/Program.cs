using Microsoft.EntityFrameworkCore;
using OrdersApi.Commands;
using OrdersApi.Data;
using OrdersApi.DTOs;
using OrdersApi.Handlers;
using OrdersApi.Handlers.Interfaces;
using OrdersApi.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BasicDatabaseConnection")));
builder.Services.AddScoped<ICommandHandler<CreateOrderCommand, OrderDTO>, CreateOrderCommandHandler>();
builder.Services.AddScoped<IQueryHandler<IEnumerable<OrderDTO>>, GetOrdersQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetOrderByIdQuery, OrderDTO?>, GetOrderByIdQueryHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapPost("/api/orders", async (CreateOrderCommand order, ICommandHandler<CreateOrderCommand, OrderDTO> commandHandler) =>
{
    var cretedOrderCommand = await commandHandler.HandleAsync(order);
    return Results.Created($"/api/orders/{cretedOrderCommand.Id}", cretedOrderCommand);
});

app.MapGet("/api/orders", async (IQueryHandler<IEnumerable<OrderDTO>> queryHandler) =>
    await queryHandler.HandleAsync());

app.MapGet("/api/orders/{id}", async (int id, IQueryHandler<GetOrderByIdQuery, OrderDTO?> queryHandler) =>
{
    var order = await queryHandler.HandleAsync(new GetOrderByIdQuery(id));
    return order is not null
        ? Results.Ok(order)
        : Results.NotFound();
});

app.Run();
