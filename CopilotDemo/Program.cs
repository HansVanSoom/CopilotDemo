using Azure.Data.Tables;
using CopilotDemo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddLogging(); // Registers logging

// Register TableClient and UserHashService with DI
builder.Services.AddSingleton<TableClient>(sp =>
    new TableClient("REDACTED", "UserHashes"));
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


