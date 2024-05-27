using System.Net.Http.Json;

using SecurityCenterCli.Authentication;
using SecurityCenterCli.Common;
using SecurityCenterCli.Infrastructure;

namespace SecurityCenterCli.Service;

internal sealed class DefenderApiService(TokenClient tokenClient, IHttpClientFactory httpClientFactory) : BaseService(tokenClient, httpClientFactory)
{
    private readonly TokenClient _tokenClient = tokenClient;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private const string ApiUrl = "https://api-eu.securitycenter.microsoft.com/api";
    private readonly string[] Scopes = ["https://api.securitycenter.microsoft.com/.default"];
    internal async Task<Result<IEnumerable<Device>?>> GetDevices(string? nameFilter)
    {
        var client = await GetAuthenticatedClient(Scopes);
        var filter = string.Empty;
        if (nameFilter is not null)
        {
            filter = $"$filter=startswith(computerDnsName,'{nameFilter}')";
        }
        var result = await client.GetAsync($"{ApiUrl}/machines?{filter}");
        var devices = await result.Content.ReadFromJsonAsync(SourceGenerationContext.Default.ODataResponseDevice);
        return devices?.Value;
    }

    internal async Task<Result<IEnumerable<DeviceVulnerability>?>> GetDeviceVulnerabilities(string deviceId)
    {
        var client = await GetAuthenticatedClient(Scopes);
        var result = await client.GetAsync($"{ApiUrl}/machines/{deviceId}/vulnerabilities");
        var vulnerabilities = await result.Content.ReadFromJsonAsync(SourceGenerationContext.Default.ODataResponseDeviceVulnerability);
        return vulnerabilities?.Value;
    }

    internal async Task<Result<IEnumerable<DeviceRecommendation>?>> GetDeviceRecommendations(string deviceId)
    {
        var client = await GetAuthenticatedClient(Scopes);
        var result = await client.GetAsync($"{ApiUrl}/machines/{deviceId}/recommendations");
        var recommendations = await result.Content.ReadFromJsonAsync(SourceGenerationContext.Default.ODataResponseDeviceRecommendation);
        return recommendations?.Value;
    }

}
