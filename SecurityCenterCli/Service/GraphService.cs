using System.Net.Http.Json;

using SecurityCenterCli.Authentication;
using SecurityCenterCli.Common;
using SecurityCenterCli.Infrastructure;

namespace SecurityCenterCli.Service;

internal sealed class GraphService(TokenClient tokenClient, IHttpClientFactory httpClientFactory) : BaseService(tokenClient, httpClientFactory)
{
    private readonly TokenClient _tokenClient = tokenClient;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private const string ApiUrl = "https://graph.microsoft.com/beta/security";
    private readonly string[] Scopes = ["https://graph.microsoft.com/.default"];

    internal async Task<Result<IEnumerable<SecureScore>?>> GetSecureScores(int days = 5)
    {
        var client = await GetAuthenticatedClient(Scopes);
        var result = await client.GetAsync($"{ApiUrl}/secureScores?$top={days}");
        var test = await result.Content.ReadAsStringAsync();
        var secureScores = await result.Content.ReadFromJsonAsync(SourceGenerationContext.Default.ODataResponseSecureScore);
        return secureScores?.Value;
    }

    internal async Task<Result<IEnumerable<SecurityControlProfile>?>> GetSecurityControlProfiles()
    {
        var client = await GetAuthenticatedClient(Scopes);
        var profiles = new List<SecurityControlProfile>();
        var url = $"{ApiUrl}/secureScoreControlProfiles";
        do
        {
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadFromJsonAsync(SourceGenerationContext.Default.ODataResponseSecurityControlProfile);
            if (result?.Value is not null)
            {
                profiles.AddRange(result.Value);
            }
            url = result?.NextLink ?? string.Empty;
        } while (!string.IsNullOrEmpty(url));

        return profiles;
    }

}