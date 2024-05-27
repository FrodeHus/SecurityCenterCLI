using System.Text.Json;

using QuiCLI.Command;

using SecurityCenterCli.Infrastructure;
using SecurityCenterCli.Service;

namespace SecurityCenterCli.Command;

internal class SecureScoreCommands(GraphService graphService)
{
    private readonly GraphService _graphService = graphService;

    [Command("list", "Retrieve secure scores")]
    public async Task SecureScoreList(int days = 5)
    {
        var result = await _graphService.GetSecureScores(days);

        if (result.IsFailure)
        {
            Console.WriteLine(result.Error);
            return;
        }

        var json = JsonSerializer.Serialize(result.Value, SourceGenerationContext.Default.SecureScoreArray);
        Console.WriteLine(json);
    }

    [Command("profiles", "Retrieve security control profiles")]
    public async Task GetSecurityControlProfiles()
    {
        var result = await _graphService.GetSecurityControlProfiles();

        if (result.IsFailure)
        {
            Console.WriteLine(result.Error);
            return;
        }

        var json = JsonSerializer.Serialize(result.Value, SourceGenerationContext.Default.SecurityControlProfileArray);
        Console.WriteLine(json);
    }

}
