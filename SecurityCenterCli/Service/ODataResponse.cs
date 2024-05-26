using System.Text.Json.Serialization;
namespace SecurityCenterCli.Service;

public record ODataResponse<T>([property: JsonPropertyName("value")] T[] Value, [property: JsonPropertyName("@odata.nextLink")] string NextLink);
