using Microsoft.EntityFrameworkCore;
using Zeiss.ProductApi.Data;
using Zeiss.ProductApi.Endpoints;
using Zeiss.ProductApi.Middleware;
using Zeiss.ProductApi.Repositories;
using Zeiss.ProductApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();  // Logs to the console (for development or debugging)
builder.Logging.AddDebug();    // Adds debug-level logging
builder.Logging.AddEventSourceLogger(); // Adds logging to EventSource (for Windows)

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISqlSequenceProvider, SqlSequenceProvider>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Register exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Register the API endpoints
app.MapProductEndpoints();

// Set up Swagger for API documentation
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();