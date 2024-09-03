
using GrpcConfig.Service;

namespace GrpcConfig.Sdk;

public class ConfigGrpcService : IConfigGrpcService
{
    private readonly Configs.ConfigsClient _client;
    public ConfigGrpcService(Configs.ConfigsClient client) 
    {
        _client = client;
    }
    public async Task<List<GetTenantConfigResponse>> GetAllTenantConfigsAsync(CancellationToken cancellationToken)
    {
        var result = await _client.GetAllTenantConfigsAsync(new GetAllTenantConfigsRequest(), null, null, cancellationToken);
        return result.TenantConfigs.ToList();
    }
}
