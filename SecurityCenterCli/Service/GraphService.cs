using System.Net.Http.Json;

using SecurityCenterCli.Authentication;
using SecurityCenterCli.Common;

namespace SecurityCenterCli.Service;

internal class GraphService(TokenClient tokenClient, IHttpClientFactory httpClientFactory)
{
    private readonly TokenClient _tokenClient = tokenClient;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private const string ApiUrl = "https://graph.microsoft.com/beta/security";

    internal async Task<Result<IEnumerable<SecureScore>?>> GetSecureScores(int days = 5)
    {
        var client = await GetAuthenticatedClient();
        var result = await client.GetAsync($"{ApiUrl}/secureScores?$top={days}");
        var secureScores = await result.Content.ReadFromJsonAsync<ODataResponse<SecureScore>>();
        return secureScores?.Value;
    }

    private async Task<HttpClient> GetAuthenticatedClient()
    {
        var tokenResult = await _tokenClient.GetAccessTokenSilently(["https://graph.microsoft.com/.default"]);
        if (tokenResult.IsFailure)
        {
            throw new InvalidOperationException(tokenResult.Error!.ToString());
        }

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenResult.Value);
        return client;
    }
}