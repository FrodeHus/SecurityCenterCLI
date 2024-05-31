using System.Text;

using QuiCLI.Command;

using SecurityCenterCli.Authentication;
using SecurityCenterCli.Infrastructure;

namespace SecurityCenterCli.Command;

internal class TokenCommands(TokenClient tokenClient, Configuration configuration)
{
    private readonly TokenClient _tokenClient = tokenClient;
    private readonly Configuration _config = configuration;

    [Command("access-token", "Get the current access token")]
    public async Task<string> GetAccessToken(bool decode, string resource = "https://api.securitycenter.microsoft.com/.default")
    {
        var result = await _tokenClient.GetAccessTokenSilently([resource]);
        if (result.IsFailure)
        {
            Console.WriteLine(result.Error);
            return string.Empty;
        }

        if (decode)
        {
            var segments = result.Value.Split('.');
            if (segments.Length != 3)
            {
                Console.WriteLine("Invalid JWT token");
                return string.Empty;
            }
            var payload = Convert.FromBase64String(AddPadding(segments[1]));
            return Encoding.UTF8.GetString(payload);

        }
        else
        {
            return result.Value;
        }
    }

    [Command("set", "Set the credentials")]
    public void SetCredentials(string tenantId, string clientId, string? clientSecret)
    {
        _config.Credential = new Credential(tenantId, clientId, clientSecret);
        _config.Save();
    }

    private string AddPadding(string base64)
    {
        while (base64.Length % 4 != 0)
        {
            base64 += "=";
        }

        return base64;
    }
}