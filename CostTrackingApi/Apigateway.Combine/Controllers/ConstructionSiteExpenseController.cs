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


        [HttpGet("cs/expenses")]
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
                    var responseB = await clientB.GetAsync("api/v/Expense");
                    if (responseA.IsSuccessStatusCode && responseB.IsSuccessStatusCode)
                    {
                        var csResponseJson = await responseA.Content.ReadAsStringAsync();
                        var csResponse = JsonConvert.DeserializeObject<PagedResponse<IEnumerable<ConstructionSiteDTO>>>(csResponseJson);
                        var expenseResponseJson = await responseB.Content.ReadAsStringAsync();
                        var expenseResponse = JsonConvert.DeserializeObject<PagedResponse<IEnumerable<ExpenseDTO>>>(expenseResponseJson);

                        var lista = new List<ExpenseDTO>();
                  
                        // Now you can access the data from the expenseResponse object.
                        foreach (var expense in expenseResponse.Data)
                        {
                            if (expense.ConstructionSiteId != 0)
                                lista.Add(expense);
                        }

                        var filterLista = new List<ExpenseDTO>();
                        foreach (var item in lista)
                        {
                            foreach (var cs in csResponse.Data)
                            {
                                if (item.ConstructionSiteId == cs.Id)
                                    filterLista.Add(item);
                            }
                        }

                        return Ok(filterLista);

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



        [HttpGet("cs/expenses/{csId}")]
        public async Task<IActionResult> GetCombinedDataById(int csId)
        {
            try
            {
                using (var clientA = new HttpClient())
                using (var clientB = new HttpClient())
                {
                    clientA.BaseAddress = new Uri("http://apigateway/");
                    clientB.BaseAddress = new Uri("http://apigateway/");

                    var responseA = await clientA.GetAsync($"api/v/ConstructionSite/{csId}");
                    var responseB = await clientB.GetAsync("api/v/Expense");
                    if (responseA.IsSuccessStatusCode && responseB.IsSuccessStatusCode)
                    {
                        var csResponseJson = await responseA.Content.ReadAsStringAsync();
                        var csResponse = JsonConvert.DeserializeObject<Response<ConstructionSiteDTO>>(csResponseJson);
                        var expenseResponseJson = await responseB.Content.ReadAsStringAsync();
                        var expenseResponse = JsonConvert.DeserializeObject<PagedResponse<IEnumerable<ExpenseDTO>>>(expenseResponseJson);

                        var lista = new List<ExpenseDTO>();

                        // Now you can access the data from the expenseResponse object.
                        foreach (var expense in expenseResponse.Data)
                        {
                            if (expense.ConstructionSiteId != 0 && expense.ConstructionSiteId == csResponse.Data.Id)
                                lista.Add(expense);
                        }

                        var combinedData = new
                        {
                            cs = csResponse.Data,
                            expense = lista
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
