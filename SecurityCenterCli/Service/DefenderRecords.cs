using System.Text.Json.Serialization;

namespace SecurityCenterCli.Service;

public record Device(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName(name: "computerDnsName")] string Name,
    [property: JsonPropertyName(name: "osPlatform")] string OSPlatform,
    [property: JsonPropertyName(name: "version")] string Version,
    [property: JsonPropertyName(name: "healthStatus")] string HealthStatus,
    [property: JsonPropertyName(name: "onboardingStatus")] string OnboardingStatus,
    [property: JsonPropertyName(name: "exposureLevel")] string ExposureLevel)
{
    public override string ToString()
    {
        return $"Device: {Id} - {Name} - {OSPlatform} - {Version} - {HealthStatus} - {OnboardingStatus} - {ExposureLevel}";
    }
}

public record DeviceVulnerability(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("severity")] string Severity,
    [property: JsonPropertyName("exposedMachines")] int ExposedMachines,
    [property: JsonPropertyName("cvssV3")] double CvssV3,
    [property: JsonPropertyName("publishedOn")] DateTime PublishedOn,
    [property: JsonPropertyName("updatedOn")] DateTime UpdatedOn,
    [property: JsonPropertyName("publicExploit")] bool PublicExploit,
    [property: JsonPropertyName("exploitTypes")] string[] ExploitTypes,
    [property: JsonPropertyName("exploitUris")] string[] ExploitUris
    );

public record DeviceRecommendation(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("productName")] string ProductName,
    [property: JsonPropertyName("recommendationName")] string RecommendationName,
    [property: JsonPropertyName("recommendedVersion")] string RecommendedVersion,
    [property: JsonPropertyName("recommendationCategory")] string RecommendationCategory,
    [property: JsonPropertyName("subCategory")] string SubCategory,
    [property: JsonPropertyName("remediationType")] string RemediationType,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("severityScore")] double SeverityScore,
    [property: JsonPropertyName("configScoreImpact")] double ConfigScoreImpact,
    [property: JsonPropertyName("exposureImpact")] double ExposureImpact,
    [property: JsonPropertyName("publicExploit")] bool PublicExploit
    );