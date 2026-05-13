using Microsoft.Extensions.DependencyInjection;
using VehicleManager.Core.Services.Abstractions;
using VehicleManager.Infra.Services;

namespace VehicleManager.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        services.AddTransient<IMileageService, MileageService>();
        services.AddTransient<IVehicleManagerService, VehicleManagerService>();
        return services;
    }
}
