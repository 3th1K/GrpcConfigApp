using GrpcConfig.Sdk;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrpcConfig.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrpcConfigController : ControllerBase
    {
        private readonly IConfigGrpcService _tenantConfigService;
        public GrpcConfigController(IConfigGrpcService tenantConfigService)
        {
            _tenantConfigService = tenantConfigService;

        }


        [HttpGet]
        [Route("configs")]
        public async Task<IActionResult> GetAllTenantConfigs(CancellationToken cancellationToken)
        {
            var result = await _tenantConfigService.GetAllTenantConfigsAsync(cancellationToken);
            return Ok(result);
        }
    }
}
