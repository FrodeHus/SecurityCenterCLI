using System.Text.Json.Serialization;

namespace SecurityCenterCli.Service;

internal record SecureScore(
     [property: JsonPropertyName("id")] string Id,
     [property: JsonPropertyName("activeUserCount")] int ActiveUserCount,
     [property: JsonPropertyName("currentScore")] double CurrentScore,
     [property: JsonPropertyName("maxScore")] double MaxScore,
     [property: JsonPropertyName("createdDateTime")] DateTime CreatedDate
    );