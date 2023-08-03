using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.WebAPI.Controllers
{
    [ApiController]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/v/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private ISender _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}
