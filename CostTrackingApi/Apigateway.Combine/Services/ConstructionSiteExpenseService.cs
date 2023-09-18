using Apigateway.Combine.Interfaces;
using ConstructionSite.Application.DTOs.ConstructionSite;
using Finance.Application.DTOs.Expense;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ResponseInfo.Entities;

namespace Apigateway.Combine.Services
{
    public class ConstructionSiteExpenseService : IConstructionSiteExpenseService
    {

        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;
        private string _accessToken = "";
        public ConstructionSiteExpenseService(HttpClient httpClient, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://apigateway/");
            _contextAccessor = contextAccessor;
            _accessToken = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
        }


        public async Task<object> GetConstructionSiteExpense(int constructionSiteId)
        {

            try
            {
                var responseA = await _httpClient.GetAsync($"api/v/ConstructionSite/{constructionSiteId}");
                var responseB = await _httpClient.GetAsync("api/v/Expense");

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

                    return combinedData;

                }

                else if (responseA.StatusCode == System.Net.HttpStatusCode.Unauthorized
                    || responseB.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Cannot access");
                }
                else if (responseA.StatusCode == System.Net.HttpStatusCode.Forbidden
                   || responseB.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    throw new HttpRequestException("Forbidden"); // You can provide a more descriptive message
                }

                else
                {
                    Console.WriteLine("Request to ConstructionSite or Supplier failed.");
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<List<ExpenseDTO>> GetConstructionSiteExpenses()
        {
            try
            {

                var responseA = await _httpClient.GetAsync($"api/v/ConstructionSite");
                var responseB = await _httpClient.GetAsync("api/v/Expense");


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

                    return filterLista;

                }

                else
                {
                    Console.WriteLine("Request to ConstructionSite or Supplier failed.");
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
