using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrdersApi.Commands;
using OrdersApi.Data;
using OrdersApi.DTOs;
using OrdersApi.Events;
using OrdersApi.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<ReadAppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ReadDatabaseConnection")));
builder.Services.AddDbContext<WriteAppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("WriteDatabaseConnection")));
builder.Services.AddScoped<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Program)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapPost("/api/orders", async (CreateOrderCommand order, IRequestHandler<CreateOrderCommand, OrderDTO> commandHandler) =>
{
    try
    {
        var cretedOrderCommand = await commandHandler.Handle(order, cancellationToken: default);
        if (cretedOrderCommand is null)
        {
            return Results.BadRequest("Failed to create order.");
        }
        return Results.Created($"/api/orders/{cretedOrderCommand.Id}", cretedOrderCommand);

    }
    catch (ValidationException ex)
    {
        var errors = ex.Errors.Select(x => x.ErrorMessage);
        return Results.BadRequest(errors);
    }

});

app.MapGet("/api/orders", async (IRequestHandler<GetOrdersQuery, IEnumerable<OrderDTO>> queryHandler) =>
    await queryHandler.Handle(new GetOrdersQuery(), cancellationToken: default));

app.MapGet("/api/orders/{id}", async (int id, IRequestHandler<GetOrderByIdQuery, OrderDTO?> queryHandler) =>
{
    var order = await queryHandler.Handle(new GetOrderByIdQuery(id), cancellationToken: default);
    return order is not null
        ? Results.Ok(order)
        : Results.NotFound();
});

app.Run();
