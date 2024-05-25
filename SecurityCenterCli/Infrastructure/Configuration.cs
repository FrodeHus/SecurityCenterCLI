using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Identity.Client.Extensions.Msal;

namespace SecurityCenterCli.Infrastructure;

[JsonSourceGenerationOptions(WriteIndented = true)]
public record Credential(string TenantId, string ClientId, string? ClientSecret);

[JsonSourceGenerationOptions(WriteIndented = true)]
public class Configuration
{
    public string ApplicationHome { get; init; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SecurityCenterCLI");
    public string AppSettingsFile => Path.Combine(ApplicationHome, "appsettings.json");
    public Credential? Credential { get; set; }
    [JsonIgnore]
    public StorageCreationProperties TokenCache
    {
        get
        {
            return new StorageCreationPropertiesBuilder("tokenCache", ApplicationHome).Build();
        }
    }

    public void EnsureApplicationHome()
    {
        if (!Directory.Exists(ApplicationHome))
        {
            Directory.CreateDirectory(ApplicationHome);
        }
    }

    public static Configuration Load()
    {
        var config = new Configuration();
        config.EnsureApplicationHome();
        if (File.Exists(config.AppSettingsFile))
        {
            var json = File.ReadAllText(config.AppSettingsFile);
            var configData = JsonSerializer.Deserialize<Configuration>(json);
            if (configData is not null)
            {
                return configData;
            }
        }
        return config;
    }

    public void Save()
    {
        var json = JsonSerializer.Serialize(this);
        File.WriteAllText(AppSettingsFile, json);
    }
}