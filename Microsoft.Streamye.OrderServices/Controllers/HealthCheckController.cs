using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Streamye.OrderServices.Controllers
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