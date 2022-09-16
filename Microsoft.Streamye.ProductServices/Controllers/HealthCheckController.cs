using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Streamye.ProductServices.Controllers
{
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetHealthCheck()
        {
            return Ok("connection normal");
        }
    }
}