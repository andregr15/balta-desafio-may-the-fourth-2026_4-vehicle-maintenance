using VehicleManager.Core.Providers.Abstractions;

namespace VehicleManager.Ai.Providers;

public class FilePromptProvider : IPromptProvider
{
    public async Task<string> GetPromptAsync(string agentName, CancellationToken cancellationToken = default)
    {
        var assembly = typeof(FilePromptProvider).Assembly;
        var resourceName = $"VehicleManager.Ai.Prompts.{agentName}.md";

        using var stream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new FileNotFoundException($"Prompt file '{resourceName}' not found as an embedded resource.");

        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync(cancellationToken);
    }
}
