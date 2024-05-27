using System.Text.Json;

using QuiCLI.Command;

using SecurityCenterCli.Infrastructure;
using SecurityCenterCli.Service;

namespace SecurityCenterCli.Command;

internal class DeviceCommands(DefenderApiService defenderService)
{
    private readonly DefenderApiService _defenderService = defenderService;

    [Command("list")]
    public async Task DeviceList(string? nameFilter)
    {
        var result = await _defenderService.GetDevices(nameFilter);

        if (result.IsFailure)
        {
            Console.WriteLine(result.Error);
            return;
        }

        var json = JsonSerializer.Serialize(result.Value, SourceGenerationContext.Default.DeviceArray);
        Console.WriteLine(json);
    }

    [Command("vulnerabilities",  "Retrieve all security vulnerabilities for device")]
    public async Task VulnerabilityList(string deviceId)
    {
        var result = await _defenderService.GetDeviceVulnerabilities(deviceId);

        if (result.IsFailure)
        {
            Console.WriteLine(result.Error);
            return;
        }

        var json = JsonSerializer.Serialize(result.Value, SourceGenerationContext.Default.DeviceVulnerabilityArray);
        Console.WriteLine(json);
    }

    [Command("recommendations", "Retrieve all security recommendations for device")]
    public async Task RecommendationList(string deviceId)
    {
        var result = await _defenderService.GetDeviceRecommendations(deviceId);

        if (result.IsFailure)
        {
            Console.WriteLine(result.Error);
            return;
        }

        var json = JsonSerializer.Serialize(result.Value, SourceGenerationContext.Default.DeviceRecommendationArray);
        Console.WriteLine(json);
    }
}
