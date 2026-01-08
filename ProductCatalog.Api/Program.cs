using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Data;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Check if running Azure
var keyVaultName = builder.Configuration["KeyVaultName"];

if (!string.IsNullOrEmpty(keyVaultName))
{
    Console.WriteLine($"Connecting to Key Vault: {keyVaultName}");
        
    var keyVaultUrl = new Uri($"https://{keyVaultName}.vault.azure.net/");
    var client = new SecretClient(keyVaultUrl, new DefaultAzureCredential());
    try
    {
        var secret = client.GetSecretAsync("SqlConnectionString").Result;
        var connectionString = secret.Value.Value;

        Console.WriteLine("Successfully retrieved SQL connection string from Key Vault");
        
        //Use SQL Server
        builder.Services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(connectionString));
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error accessing Key Vault: {ex.Message}");
        throw;
    }
}
else
{
    // Running locally
    Console.WriteLine("Running locally with SQLite");
    
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=products.db";

    builder.Services.AddDbContext<ProductDbContext>(options => options.UseSqlite(connectionString));
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

/*
app.Urls.Add("http://0.0.0.0:8080");

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
*/




/*
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();
*/

/*
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
*/

// Ensure database is created and seeded
/*
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    context.Database.EnsureCreated();
}
*/

// Enable Swagger in all environments (for testing)
app.UseSwagger();
app.UseSwaggerUI();

// Only redirect to HTTPS when not in a container
//if (!app.Environment.IsDevelopment())
//{
    // Comment out HTTPS redirection for now
    // app.UseHttpsRedirection();
//}

app.UseAuthorization();
app.MapControllers();
Console.WriteLine("Starting application on port 8080...");
app.Run("http://0.0.0.0:8080");
