using System.Net.Http.Json;

using SecurityCenterCli.Authentication;
using SecurityCenterCli.Common;

namespace SecurityCenterCli.Service;

internal class DefenderApiService(TokenClient tokenClient, IHttpClientFactory httpClientFactory)
{
    private readonly TokenClient _tokenClient = tokenClient;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private const string ApiUrl = "https://api-eu.securitycenter.microsoft.com/api";
    internal async Task<Result<IEnumerable<Device>?>> GetDevices(string? nameFilter)
    {
        var client = await GetAuthenticatedClient();
        var filter = string.Empty;
        if (nameFilter is not null)
        {
            filter = $"$filter=startswith(computerDnsName,'{nameFilter}')";
        }
        var result = await client.GetAsync($"{ApiUrl}/machines?{filter}");
        var devices = await result.Content.ReadFromJsonAsync<ODataResponse<Device>>();
        return devices?.Value;
    }

    internal async Task<Result<IEnumerable<DeviceVulnerability>?>> GetDeviceVulnerabilities(string deviceId)
    {
        var client = await GetAuthenticatedClient();
        var result = await client.GetAsync($"{ApiUrl}/machines/{deviceId}/vulnerabilities");
        var vulnerabilities = await result.Content.ReadFromJsonAsync<ODataResponse<DeviceVulnerability>>();
        return vulnerabilities?.Value;
    }

    internal async Task<Result<IEnumerable<DeviceRecommendation>?>> GetDeviceRecommendations(string deviceId)
    {
        var client = await GetAuthenticatedClient();
        var result = await client.GetAsync($"{ApiUrl}/machines/{deviceId}/recommendations");
        var recommendations = await result.Content.ReadFromJsonAsync<ODataResponse<DeviceRecommendation>>();
        return recommendations?.Value;
    }


    private async Task<HttpClient> GetAuthenticatedClient()
    {
        var tokenResult = await _tokenClient.GetAccessTokenSilently();
        if (tokenResult.IsFailure)
        {
            throw new InvalidOperationException(tokenResult.Error!.ToString());
        }

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenResult.Value);
        return client;
    }
}
