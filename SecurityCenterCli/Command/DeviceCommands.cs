using System.Text.Json;

using QuiCLI.Command;

using SecurityCenterCli.Infrastructure;
using SecurityCenterCli.Service;

namespace SecurityCenterCli.Command;

internal class DeviceCommands(DefenderApiService defenderService)
{
    private readonly DefenderApiService _defenderService = defenderService;

    [Command("list")]
    public async Task<object> DeviceList(string? nameFilter)
    {
        var result = await _defenderService.GetDevices(nameFilter);

        if (result.IsFailure)
        {
            return result.Error!;
        }

        return JsonSerializer.Serialize(result.Value, SourceGenerationContext.Default.DeviceArray);
    }

    [Command("vulnerabilities",  "Retrieve all security vulnerabilities for device")]
    public async Task<object> VulnerabilityList(string deviceId)
    {
        var result = await _defenderService.GetDeviceVulnerabilities(deviceId);

        if (result.IsFailure)
        {
            return result.Error!;
        }

        return JsonSerializer.Serialize(result.Value, SourceGenerationContext.Default.DeviceVulnerabilityArray);
    }

    [Command("recommendations", "Retrieve all security recommendations for device")]
    public async Task<object> RecommendationList(string deviceId)
    {
        var result = await _defenderService.GetDeviceRecommendations(deviceId);

        if (result.IsFailure)
        {
            return result.Error!;
        }

        return JsonSerializer.Serialize(result.Value, SourceGenerationContext.Default.DeviceRecommendationArray);
    }
}
