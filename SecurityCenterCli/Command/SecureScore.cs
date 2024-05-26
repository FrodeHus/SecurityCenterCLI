using System.Text.Json;

using Cocona;

using SecurityCenterCli.Service;

namespace SecurityCenterCli.Command;

internal class SecureScore(GraphService graphService)
{
    private readonly GraphService _graphService = graphService;

    [Command("list", Description = "Retrieve secure scores")]
    public async Task SecureScoreList(int days = 5)
    {
        var result = await _graphService.GetSecureScores(days);

        if (result.IsFailure)
        {
            Console.WriteLine(result.Error);
            return;
        }

        var json = JsonSerializer.Serialize(result.Value, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);
    }

    [Command("profiles", Description = "Retrieve security control profiles")]
    public async Task GetSecurityControlProfiles()
    {
        var result = await _graphService.GetSecurityControlProfiles();

        if (result.IsFailure)
        {
            Console.WriteLine(result.Error);
            return;
        }

        var json = JsonSerializer.Serialize(result.Value, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);
    }

}
