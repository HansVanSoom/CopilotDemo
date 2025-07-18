using Azure.Data.Tables;
using CopilotDemo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddLogging(); // Registers logging

// Get storage key from environment variable
var storageKey = Environment.GetEnvironmentVariable("AZURE_STORAGE_KEY");
if (string.IsNullOrWhiteSpace(storageKey))
{
    throw new InvalidOperationException("AZURE_STORAGE_KEY not set in environment variables.");
}

// Register TableClient and UserHashService with DI
builder.Services.AddSingleton<TableClient>(sp =>
    new TableClient($"DefaultEndpointsProtocol=https;AccountName=storageaccounthvs2;AccountKey={storageKey};EndpointSuffix=core.windows.net", "UserHashes"));
builder.Services.AddScoped<UserHashService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


