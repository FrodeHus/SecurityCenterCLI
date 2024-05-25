using System.Text.Json;

using Cocona;

using SecurityCenterCli.Service;

namespace SecurityCenterCli.Command;

internal class SecureScoreCommands(GraphService graphService)
{
    private readonly GraphService _graphService = graphService;

    [Command("secure-score", Description = "Retrieve secure scores")]
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

    //[Command("details", Description = "Retrieve secure score details")]
    //public async Task SecureScoreDetails(string secureScoreId)
    //{
    //    var result = await _graphService.GetSecureScoreDetails(secureScoreId);

    //    if (result.IsFailure)
    //    {
    //        Console.WriteLine(result.Error);
    //        return;
    //    }

    //    var json = JsonSerializer.Serialize(result.Value, new JsonSerializerOptions { WriteIndented = true });
    //    Console.WriteLine(json);
    //}

}
