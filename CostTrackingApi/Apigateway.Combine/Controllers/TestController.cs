using Apigateway.Combine.Controllers;
using ConstructionSite.Application.DTOs.ConstructionSite;
using ConstructionSite.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Storage.Application.DTOs.Supplier;

namespace ApiGateway.Combine.Controllers
{


    public class TestController : BaseApiController
    { 

        [HttpGet("combined")]
        public async Task<IActionResult> GetCombinedData()
        {
            try
            {
                using (var clientA = new HttpClient())
                using (var clientB = new HttpClient())
                {
                    clientA.BaseAddress = new Uri("http://apigateway/");
                    clientB.BaseAddress = new Uri("http://apigateway/");

                    var responseA = await clientA.GetAsync("api/v/ConstructionSite");
                    var responseB = await clientB.GetAsync("api/v/Supplier");

                    if (responseA.IsSuccessStatusCode && responseB.IsSuccessStatusCode)
                    {
                        var contentA = await responseA.Content.ReadAsStringAsync();
                        var contentB = await responseB.Content.ReadAsStringAsync();

                        var dataFromServiceA = JsonConvert.DeserializeObject<PagedResponse<IEnumerable<ConstructionSiteDTO>>>(contentA);
                        var dataFromServiceB = JsonConvert.DeserializeObject<PagedResponse<IEnumerable<SupplierDTO>>>(contentB);


                        var combinedData = new
                        {
                            c = dataFromServiceA,
                            s = dataFromServiceB
                        };

                        return Ok(combinedData);
                    }
                    else
                    {
                        Console.WriteLine("Request to ConstructionSite or Supplier failed.");
                        return StatusCode(500); 
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", ExceptionMessage = ex.Message });
            }
        }
    }
}
