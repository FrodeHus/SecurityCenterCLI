using System.Text.Json;

using Cocona;

using SecurityCenterCli.Service;

namespace SecurityCenterCli.Command;

[HasSubCommands(typeof(Device), Description = "Perform operations related to devices")]
internal class DefenderCommands
{
    
}

internal class Device(DefenderApiService defenderService)
{
    private readonly DefenderApiService _defenderService = defenderService;

    [Command("list")]
    public async Task DeviceList([Option(Description = "Filter on devices starting with this name")]string? nameFilter)
    {
        var result = await _defenderService.GetDevices(nameFilter);

        if (result.IsFailure)
        {
            Console.WriteLine(result.Error);
            return;
        }

        var json = JsonSerializer.Serialize(result.Value, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);
    }

    [Command("vulnerabilities", Description = "Retrieve all security vulnerabilities for device")]
    public async Task VulnerabilityList(string deviceId)
    {
        var result = await _defenderService.GetDeviceVulnerabilities(deviceId);

        if (result.IsFailure)
        {
            Console.WriteLine(result.Error);
            return;
        }

        var json = JsonSerializer.Serialize(result.Value, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);
    }

    [Command("recommendations", Description = "Retrieve all security recommendations for device")]
    public async Task RecommendationList(string deviceId)
    {
        var result = await _defenderService.GetDeviceRecommendations(deviceId);

        if (result.IsFailure)
        {
            Console.WriteLine(result.Error);
            return;
        }

        var json = JsonSerializer.Serialize(result.Value, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);
    }
}
