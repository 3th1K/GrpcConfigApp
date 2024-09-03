
using GrpcConfig.Service;
using Microsoft.Extensions.DependencyInjection;

namespace GrpcConfig.Sdk;

public static class ServiceCollectionExtensions
{
    public static void AddGrpcSdk(this IServiceCollection services)
    {
        services.AddGrpcClient<Configs.ConfigsClient>(client =>
        {
            client.Address = new Uri("https://localhost:7150");
        });

        services.AddScoped<IConfigGrpcService, ConfigGrpcService>();
    }
}
