using System.Text.Json.Serialization;

using SecurityCenterCli.Service;

namespace SecurityCenterCli.Infrastructure
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(ODataResponse<Device>))]
    [JsonSerializable(typeof(ODataResponse<SecureScore>))]
    [JsonSerializable(typeof(ODataResponse<SecurityControlProfile>))]
    [JsonSerializable(typeof(ODataResponse<SecurityControlScore>))]
    [JsonSerializable(typeof(ODataResponse<DeviceVulnerability>))]
    [JsonSerializable(typeof(ODataResponse<DeviceRecommendation>))]
    [JsonSerializable(typeof(Configuration))]
    [JsonSerializable(typeof(Credential))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(string[]))]
    [JsonSerializable(typeof(int))]
    [JsonSerializable(typeof(double))]
    [JsonSerializable(typeof(DateTime))]
    [JsonSerializable(typeof(bool))]
    [JsonSerializable(typeof(Device))]
    [JsonSerializable(typeof(DeviceVulnerability))]
    [JsonSerializable(typeof(DeviceRecommendation))]
    [JsonSerializable(typeof(SecureScore))]
    [JsonSerializable(typeof(SecurityControlProfile))]
    [JsonSerializable(typeof(SecurityControlScore))]
    internal partial class SourceGenerationContext : JsonSerializerContext
    {
    }
}
