using Microsoft.Extensions.Logging;
using VehicleManager.Core.Agents.Abstractions;
using VehicleManager.Core.Models;
using VehicleManager.Core.Services.Abstractions;

namespace VehicleManager.Infra.Services;

public class VehicleManagerService(
    ILogger<VehicleManagerService> logger,
    IMileageService mileageService,
    IAgent<IEnumerable<long>, IEnumerable<Maintenance>> maintenanceAgent
) : IVehicleManagerService
{
    public async Task<IEnumerable<Maintenance>> GetMaintenancesAsync(CancellationToken cancellationToken = default)
    {
        var mileages = await mileageService.GetMileages(cancellationToken);
        logger.LogInformation("Getting maintenances for mileages: {Mileages}", mileages);

        var response = await maintenanceAgent.ExecuteAsync(mileages, cancellationToken);

        logger.LogInformation("Received maintenances: {Maintenances}", response);

        return response;
    }
}
