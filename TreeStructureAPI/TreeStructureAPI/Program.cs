using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TreeStructureAPI.Mappers;
using TreeStructureAPI.Models;
using TreeStructureAPI.Repositories;
using TreeStructureAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddAutoMapper(typeof(ItemMappingProfile));   

var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();