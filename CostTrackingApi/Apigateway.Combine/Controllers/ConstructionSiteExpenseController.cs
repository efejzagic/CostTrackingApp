using Apigateway.Combine.Interfaces;
using ConstructionSite.Application.DTOs.ConstructionSite;
using Finance.Application.DTOs.Expense;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ResponseInfo.Entities;
using Storage.Application.DTOs.Supplier;

namespace Apigateway.Combine.Controllers
{
    public class ConstructionSiteExpenseController : BaseApiController
    {

        private readonly IConstructionSiteExpenseService _constructionSiteExpenseService;


        public ConstructionSiteExpenseController(IConstructionSiteExpenseService constructionSiteExpenseService)
        {
            _constructionSiteExpenseService = constructionSiteExpenseService;
        }

        [HttpGet("cs/expenses")]
        public async Task<IActionResult> GetCombinedData()
        {
            try
            {
                var response = await _constructionSiteExpenseService.GetConstructionSiteExpenses();

                if (response != null)
                {
                    return Ok(response);
                }
                return StatusCode(500, "Unknown error");
            }

            catch (UnauthorizedAccessException unex) 
            {
                return StatusCode(401, "Unaothorized access");
            }

            catch (HttpRequestException ex)
            {
                return StatusCode(401, "Forbidden access");
            }
        }



        [HttpGet("cs/expenses/{csId}")]
        public async Task<IActionResult> GetCombinedDataById(int csId)
        {
            var response = await _constructionSiteExpenseService.GetConstructionSiteExpense(csId);

            if (response != null)
            {
                return Ok(response);
            }

            return StatusCode(500, new { Message = "An error occurred" });
        }

    }
}
