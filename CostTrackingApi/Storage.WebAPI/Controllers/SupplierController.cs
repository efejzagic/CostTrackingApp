using CorrelationIdLibrary.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Storage.Application.Features.Supplier.Commands;
using Storage.Application.Features.Supplier.Queries;
using Storage.Application.Parameters.Supplier;
using System.Net.Http;
using WebApi.Controllers;

namespace Storage.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    
    public class SupplierController : BaseApiController
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly ICorrelationIdGenerator _correlationIdGenerator;

        public SupplierController(ILogger<SupplierController> logger, ICorrelationIdGenerator correlationIdGenerator)
        {
            _logger = logger;
            _correlationIdGenerator = correlationIdGenerator;
        }

        [HttpGet]
        //[MapToApiVersion("1.0")]
        [Authorize(Roles = "Storage Manager,Finance")]
        public async Task<IActionResult> Get([FromQuery] GetAllSupplierParameter filter)
        {
            _logger.LogWarning("CorrelationId: {correlationId}" , _correlationIdGenerator.Get());

            return Ok(await Mediator.Send(new GetAllSupplierQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Storage Manager,Finance")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetSupplierByIdQuery { Id = id }));
        }

        [HttpGet("Name/{Name}")]
        [Authorize(Roles = "Storage Manager")]
        public async Task<IActionResult> Get(string name)
        {
            return Ok(await Mediator.Send(new GetSupplierByNameQuery { Name = name }));
        }

     

        
        [HttpPost]
        [Authorize(Roles = "Storage Manager")]
        public async Task<IActionResult> Post(CreateSupplierCommand command)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpPut]
        [Authorize(Roles = "Storage Manager")]
        public async Task<IActionResult> Put(UpdateSupplierCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Storage Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            var enviroment = await Mediator.Send(new DeleteSupplierCommand { Id = id });
            return Ok(enviroment);
        }

    }
}
