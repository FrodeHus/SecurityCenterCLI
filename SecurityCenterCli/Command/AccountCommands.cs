using QuiCLI.Command;

using SecurityCenterCli.Authentication;

namespace SecurityCenterCli.Command;
internal class AccountCommands(TokenClient tokenClient)
{
    private readonly TokenClient _tokenClient = tokenClient;

    [Command("login", "Log in to Microsoft Security Center")]
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
