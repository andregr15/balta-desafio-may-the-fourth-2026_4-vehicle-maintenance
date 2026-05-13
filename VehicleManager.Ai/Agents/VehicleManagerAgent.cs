using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenAI;
using OpenAI.Chat;
using VehicleManager.Ai.Models;
using VehicleManager.Core;
using VehicleManager.Core.Agents.Abstractions;
using VehicleManager.Core.Enums;
using VehicleManager.Core.Models;
using VehicleManager.Core.Providers.Abstractions;

namespace VehicleManager.Ai.Agents;

public class VehicleManagerAgent(
    ILogger<VehicleManagerAgent> logger,
    [FromKeyedServices(PromptProviderType.File)] IPromptProvider promptProvider
) : IAgent<IEnumerable<long>, IEnumerable<Maintenance>>
{
    private const float Temperature = 0.7f;
    private const string Prompt = "Kilometragens: ";

    public async Task<IEnumerable<Maintenance>> ExecuteAsync(
        IEnumerable<long> data,
        CancellationToken cancellationToken = default
    )
    {
        logger.LogInformation("Executing VehicleManagerAgent with {Count} mileages.", data.Count());

        var client = new OpenAIClient(Configuration.OpenApiKey);
        var instructions = await promptProvider.GetPromptAsync(nameof(VehicleManagerAgent), cancellationToken);

        var agent = client
            .GetChatClient(AiModels.Gpt4OMini)
            .AsAIAgent(new ChatClientAgentOptions
            {
                Name = nameof(VehicleManagerAgent),
                Description = "Agent responsible for analyzing vehicle mileages and determining necessary maintenance tasks.",
                ChatOptions = new ChatOptions
                {
                    Temperature = Temperature,
                    Instructions = instructions
                },
            });

        var prompt = $"{Prompt}{string.Join(", ", data)}";

        logger.LogInformation("Sending prompt to AI: {Prompt}", prompt);

        var response = await agent
            .RunAsync<IEnumerable<Maintenance>>(
                prompt,
                cancellationToken: cancellationToken
            );

        logger.LogInformation("Received response from AI: {Response}", response);

        return response.Result;
    }
}
