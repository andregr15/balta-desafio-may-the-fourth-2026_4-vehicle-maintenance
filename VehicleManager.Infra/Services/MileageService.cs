using VehicleManager.Core.Services.Abstractions;

namespace VehicleManager.Infra.Services;

public class MileageService : IMileageService
{
    public Task<IEnumerable<long>> GetMileages(CancellationToken cancellationToken = default)
    {
        var assembly = typeof(MileageService).Assembly;
        var resourceName = $"VehicleManager.Infra.Resources.Mileages.csv";

        using var csvStream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Resource '{resourceName}' not found.");

        using var reader = new StreamReader(csvStream);

        var mileages = new List<long>();

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var value = line?.Split(';').FirstOrDefault();

            if (long.TryParse(value ?? line, out var mileage))
            {
                mileages.Add(mileage);
            }
        }

        return Task.FromResult<IEnumerable<long>>(mileages);
    }
}
