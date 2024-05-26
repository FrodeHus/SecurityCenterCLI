using System.Text;

using Cocona;
using SecurityCenterCli.Authentication;
using SecurityCenterCli.Infrastructure;

namespace SecurityCenterCli.Command;
[HasSubCommands(typeof(TokenCommands), commandName: "account")]
internal class AccountCommands(TokenClient tokenClient)
{
    private readonly TokenClient _tokenClient = tokenClient;

    [Command("login", Description = "Log in to Microsoft Security Center")]
    public async Task Login()
    {
        var result = await _tokenClient.GetAccessToken();
        if (result.IsFailure)
        {
            Console.WriteLine(result.Error);
            return;
        }
        Console.WriteLine("Login successful");
    }
}

internal class TokenCommands(TokenClient tokenClient, Configuration configuration)
{
    private readonly TokenClient _tokenClient = tokenClient;
    private readonly Configuration _config = configuration;

    [Command("access-token", Description = "Get the current access token")]
    public async Task GetAccessToken(bool decode, string resource = "https://api.securitycenter.microsoft.com/.default")
    {
        var result = await _tokenClient.GetAccessTokenSilently([resource]);
        if (result.IsFailure)
        {
            Console.WriteLine(result.Error);
            return;
        }

        if (decode)
        {
            var segments = result.Value.Split('.');
            if (segments.Length != 3)
            {
                Console.WriteLine("Invalid JWT token");
                return;
            }
            var payload = Convert.FromBase64String(AddPadding(segments[1]));
            Console.WriteLine(Encoding.UTF8.GetString(payload));
            return;
        }
        else
        {
            Console.WriteLine(result.Value);
        }
    }

    [Command("set", Description = "Set the credentials")]
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