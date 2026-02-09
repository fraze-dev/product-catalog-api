using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Data;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Get connection string from environment or configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (!string.IsNullOrEmpty(connectionString) && connectionString.Contains("database.windows.net"))
{
    Console.WriteLine("Using SQL Server (Azure)");
    builder.Services.AddDbContext<ProductDbContext>(options => 
        options.UseSqlServer(connectionString));
}
else
{
    Console.WriteLine("Using SQLite (Local Development)");
    connectionString = connectionString ?? "Data Source=products.db";
    builder.Services.AddDbContext<ProductDbContext>(options => 
        options.UseSqlite(connectionString));
}

var app = builder.Build();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
        Console.WriteLine("Creating/ensuring database exists...");
        context.Database.EnsureCreated();
        Console.WriteLine("Database ready!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error creating database: {ex.Message}");
        throw;
    }
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
Console.WriteLine("Starting application on port 8080...");
app.Run("http://0.0.0.0:8080");