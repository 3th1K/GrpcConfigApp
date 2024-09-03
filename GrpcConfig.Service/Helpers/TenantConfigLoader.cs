using System.Text.Json;
using GrpcConfig.Service.Models;


namespace GrpcConfig.Service.Helpers;

public static class TenantConfigLoader
{
    public static Dictionary<string, TenantConfig> LoadTenantConfigs()
    {
        var tenantConfigs = new Dictionary<string, TenantConfig>(StringComparer.OrdinalIgnoreCase);
        var environmentVariables = Environment.GetEnvironmentVariables();

        foreach (var key in environmentVariables.Keys)
        {
            if (key is string keyString && keyString.StartsWith("TENANT_CONFIG_"))
            {
                var tenantId = keyString.Replace("TENANT_CONFIG_", "");
                var jsonValue = Environment.GetEnvironmentVariable(keyString);

                if (!string.IsNullOrWhiteSpace(jsonValue))
                {
                    try
                    {
                        var config = JsonSerializer.Deserialize<TenantConfig>(jsonValue, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                            ReadCommentHandling = JsonCommentHandling.Skip,
                            AllowTrailingCommas = true
                        });

                        if (config != null)
                        {
                            config.TenantId ??= tenantId; // Assign tenantId if it's not already set in the config
                            tenantConfigs[tenantId] = config;
                        }
                        else
                        {
                            Console.WriteLine($"Warning: Tenant config for {tenantId} is null after deserialization.");
                        }
                    }
                    catch (JsonException jsonEx)
                    {
                        Console.WriteLine($"Error parsing JSON for tenant {tenantId}: {jsonEx.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unexpected error loading config for tenant {tenantId}: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Warning: Environment variable {keyString} is empty or null.");
                }
            }
        }

        return tenantConfigs;
    }
}

