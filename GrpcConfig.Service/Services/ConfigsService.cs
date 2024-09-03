using Grpc.Core;
using GrpcConfig.Service.Models;

namespace GrpcConfig.Service.Services
{
    public class ConfigsService : Configs.ConfigsBase
    {
        private readonly Dictionary<string, TenantConfig> _tenantConfigs;
        public ConfigsService(Dictionary<string, TenantConfig> tenantConfigs)
        {
            _tenantConfigs = tenantConfigs;
        }
        public override Task<GetAllTenantConfigsResponse> GetAllTenantConfigs(GetAllTenantConfigsRequest request, ServerCallContext context)
        {
            var configs = _tenantConfigs.Values.ToList();
            GetAllTenantConfigsResponse res = new GetAllTenantConfigsResponse();
            res.TenantConfigs.AddRange(configs.Select(config => new GetTenantConfigResponse
            {
                TenantId = config.TenantId,
                TenantName = config.TenantName
            }));
            return Task.FromResult(res);
        }
    }
}
