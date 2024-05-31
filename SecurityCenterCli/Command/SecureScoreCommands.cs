using System.Text.Json;

using QuiCLI.Command;

using SecurityCenterCli.Infrastructure;
using SecurityCenterCli.Service;

namespace SecurityCenterCli.Command;

internal class SecureScoreCommands(GraphService graphService)
{
    private readonly GraphService _graphService = graphService;

    [Command("list", "Retrieve secure scores")]
    public async Task<object?> SecureScoreList(int days = 5)
    {
        var result = await _graphService.GetSecureScores(days);

        return result.IsFailure ? result.Error : JsonSerializer.Serialize(result.Value, SourceGenerationContext.Default.SecureScoreArray);
    }

    [Command("profiles", "Retrieve security control profiles")]
    public async Task<object?> GetSecurityControlProfiles()
    {
        var result = await _graphService.GetSecurityControlProfiles();

        return result.IsFailure
            ? result.Error
            : JsonSerializer.Serialize(result.Value!.ToArray(), SourceGenerationContext.Default.SecurityControlProfileArray);
    }

}
