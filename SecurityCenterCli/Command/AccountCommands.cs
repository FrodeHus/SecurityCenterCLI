using QuiCLI.Command;

using SecurityCenterCli.Authentication;

namespace SecurityCenterCli.Command;
internal class AccountCommands(TokenClient tokenClient)
{
    private readonly TokenClient _tokenClient = tokenClient;
    public async Task<string> Login()
    {
        var result = await _tokenClient.GetAccessToken();
        if (result.IsFailure)
        {
            return result.Error!.ToString();
        }
        return "Login successful";
    }
}
