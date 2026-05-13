using Microsoft.Extensions.DependencyInjection;
using VehicleManager.Ai.Agents;
using VehicleManager.Ai.Providers;
using VehicleManager.Core.Agents.Abstractions;
using VehicleManager.Core.Enums;
using VehicleManager.Core.Models;
using VehicleManager.Core.Providers.Abstractions;

namespace VehicleManager.Ai;

public static class DependencyInjection
{
    public static IServiceCollection AddAgents(this IServiceCollection services)
    {
        // Register AI-related services here
        services.AddKeyedTransient<IPromptProvider, FilePromptProvider>(PromptProviderType.File);
        services.AddTransient<IAgent<IEnumerable<long>, IEnumerable<Maintenance>>, VehicleManagerAgent>();

        return services;
    }
}
