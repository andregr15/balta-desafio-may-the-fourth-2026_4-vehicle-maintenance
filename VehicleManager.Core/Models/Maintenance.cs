namespace VehicleManager.Core.Models;

public class Maintenance
{
    public string Name { get; set; } = string.Empty;

    public long Mileage { get; set; }

    public IEnumerable<Part> Parts { get; set; } = [];
}
