using Microsoft.AspNetCore.Mvc;

namespace Apigateway.Combine.Controllers
{
    [ApiController]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/v/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
