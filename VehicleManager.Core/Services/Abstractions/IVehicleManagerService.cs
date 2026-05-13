using VehicleManager.Core.Models;

namespace VehicleManager.Core.Services.Abstractions;

public interface IVehicleManagerService
{
    Task<IEnumerable<Maintenance>> GetMaintenancesAsync(CancellationToken cancellationToken = default);
}
