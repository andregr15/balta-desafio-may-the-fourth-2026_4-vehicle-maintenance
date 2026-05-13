using VehicleManager.Ai;
using VehicleManager.Core;
using VehicleManager.Core.Services.Abstractions;
using VehicleManager.Infra;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfra();
builder.Services.AddAgents();

Configuration.OpenApiKey = builder.Configuration.GetValue<string>("OpenApiKey")
    ?? throw new InvalidOperationException("OpenApiKey is not configured");

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/maintenance", async (IVehicleManagerService service) =>
{
    var response = await service.GetMaintenancesAsync();
    return Results.Ok(response);
});

app.Run();
