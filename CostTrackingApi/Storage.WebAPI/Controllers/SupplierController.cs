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


        [HttpGet]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> Get([FromQuery] GetAllSupplierParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllSupplierQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetSupplierByIdQuery { Id = id }));
        }

        [HttpGet("Name/{Name}")]
        public async Task<IActionResult> Get(string name)
        {
            return Ok(await Mediator.Send(new GetSupplierByNameQuery { Name = name }));
        }

        //[HttpGet("{id}/history")]
        //public async Task<IActionResult> GetHistory(string id)
        //{
        //    return Ok(await Mediator.Send(new GetEnviromentByIdWithHistoryQuery { Id = id }));
        //}

        [HttpGet("cs")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEquipment()
        {
            try
            {
                using var httpClient = new HttpClient();
                string constructionSiteServiceUrl = "http://apigateway/api/v/ConstructionSite/test"; // Update with your actual URL

                HttpResponseMessage response = await httpClient.GetAsync(constructionSiteServiceUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return Ok(responseBody);
                }
                else
                {
                    // Log the error or handle it as needed
                    Console.WriteLine("Request to ConstructionSite failed with status code: " + response.StatusCode);
                    return StatusCode((int)response.StatusCode); // Return status code from the upstream service
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine("An error occurred: " + ex.Message);
                return StatusCode(500); // Internal Server Error
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateSupplierCommand command)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            //var enviroment = await Mediator.Send(command);
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateSupplierCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var enviroment = await Mediator.Send(new DeleteSupplierCommand { Id = id });
            return Ok(enviroment);
        }

    }
}
