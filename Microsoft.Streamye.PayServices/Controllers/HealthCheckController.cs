using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Streamye.PayServices.Controllers
{
    [Route("HealthCheck")]
    public class HealthCheckController :ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetHealthCheck()
        {
            return Ok("连接正常");
        }
    }
}