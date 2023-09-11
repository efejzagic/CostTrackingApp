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

        public ConstructionSiteExpenseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://apigateway/");
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
