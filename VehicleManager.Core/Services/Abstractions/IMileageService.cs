namespace VehicleManager.Core.Services.Abstractions;

public interface IMileageService
{
    Task<IEnumerable<long>> GetMileages(CancellationToken cancellationToken = default);
}
