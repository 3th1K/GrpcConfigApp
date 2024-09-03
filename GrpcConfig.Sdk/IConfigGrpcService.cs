using GrpcConfig.Service;

namespace GrpcConfig.Sdk;

public interface IConfigGrpcService
{
    Task<List<GetTenantConfigResponse>> GetAllTenantConfigsAsync(CancellationToken cancellationToken);
}
