using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreeStructureAPI.Mappers;
using TreeStructureAPI.Models;
using TreeStructureAPI.Repositories;
using TreeStructureAPI.Services;
using TreeStructureAPI.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddAutoMapper(typeof(ItemMappingProfile));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(ItemValidator));

var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.MapPost("/api/item", async (IItemService itemService, Item item, ItemValidator validator) =>
{
    var validationResult = await validator.ValidateAsync(item);
    if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);
    var result = await itemService.CreateItem(item);
    return result is not null ? Results.Created($"/api/item/{result}", result) : Results.BadRequest();
});

app.MapGet("/api/item", async (IItemService itemService, [FromQuery] SortDirection? sortDirection, [FromQuery] Guid? rootId) =>
{
    var result = await itemService.GetAllItems(rootId, sortDirection);
    return result.Any() ? Results.Ok(result) : Results.NotFound();

});

app.MapGet("/api/item/{id:Guid}", async (IItemService itemService, Guid id) =>
{
    var result = await itemService.GetItemById(id);
    return result is not null ? Results.Ok(result) : Results.NotFound();

});

app.MapDelete("api/item/{id:guid}", async (IItemService itemService, Guid id) =>
{
    var result = await itemService.DeleteItem(id);
    return result ? Results.NoContent() : Results.NotFound();
});

app.MapPost("api/item/{id:guid}", async (IItemService itemService, Guid id, Item item) =>
{
    var result = await itemService.UpdateItem(id, item);
    return result ? Results.NoContent() : Results.NotFound();
});

app.Run();