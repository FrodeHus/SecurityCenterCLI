using System.Text.Json.Serialization;

namespace SecurityCenterCli.Service;

internal record SecureScore(
     [property: JsonPropertyName("id")] string Id,
     [property: JsonPropertyName("activeUserCount")] int ActiveUserCount,
     [property: JsonPropertyName("currentScore")] double CurrentScore,
     [property: JsonPropertyName("maxScore")] double MaxScore,
     [property: JsonPropertyName("createdDateTime")] DateTime CreatedDate,
     [property: JsonPropertyName("controlScores")] SecurityControlScore[] ControlScores
    );

internal record SecurityControlProfile(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("actionType")] string ActionType,
    [property: JsonPropertyName("actionUrl")] string ActionUrl,
    [property: JsonPropertyName("controlCategory")] string ControlCategory,
    [property: JsonPropertyName("deprecated")] bool Deprecated,
    [property: JsonPropertyName("implementationCost")] string ImplementationCost,
    [property: JsonPropertyName("lastModifiedDateTime")] DateTime? LastModifiedDateTime,
    [property: JsonPropertyName("maxScore")] double MaxScore,
    [property: JsonPropertyName("rank")] int Rank,
    [property: JsonPropertyName("remediation")] string Remediation,
    [property: JsonPropertyName("service")] string Service,
    [property: JsonPropertyName("threats")] string[] Threats,
    [property: JsonPropertyName("tier")] string Tier,
    [property: JsonPropertyName("userImpact")] string UserImpact
    );

internal record SecurityControlScore(
    [property: JsonPropertyName("controlCategory")] string ControlCategory,
    [property: JsonPropertyName("controlName")] string ControlName,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("score")] double Score,
    [property: JsonPropertyName("scoreInPercentage")] double ScoreInPercentage,
    [property: JsonPropertyName("lastSynced")] DateTime LastSynced,
    [property: JsonPropertyName("implementationStatus")] string ImplementationStatus,
    [property: JsonPropertyName("on")] string On
    );
