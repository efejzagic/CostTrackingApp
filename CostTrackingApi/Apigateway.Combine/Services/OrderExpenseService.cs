using Apigateway.Combine.Interfaces;
using AutoMapper;
using ConstructionSite.Application.DTOs.ConstructionSite;
using ConstructionSite.Domain.Entities;
using Finance.Application.DTOs.Expense;
using Finance.Application.DTOs.ExpenseItem;
using Finance.Application.Features.Expense.Commands;
using Newtonsoft.Json;
using ResponseInfo.Entities;
using Storage.Application.DTOs.Order;
using Storage.Application.Features.Article.Commands;
using Storage.Application.Features.Order.Commands;
using System.Net.Http;

namespace Apigateway.Combine.Services
{
    public class OrderExpenseService : IOrderExpenseService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;IHttpContextAccessor _contextAccessor;
        private string _accessToken = "";
        public OrderExpenseService(HttpClient httpClient, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://apigateway/");
            _contextAccessor = contextAccessor;
            _accessToken = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
        }
        public async Task<Response<OrderDTO>> CreateExpenseByOrder(CreateOrderCommand order)
        {
            try
            {
                var orderContent = JsonContent.Create(order);

                var responseA = await _httpClient.PostAsync($"api/v/Order", orderContent);
                var orderResponseJson = await responseA.Content.ReadAsStringAsync();
                var orderResponse = JsonConvert.DeserializeObject<Response<OrderDTO>>(orderResponseJson);

                var expenseItems = new List<CreateExpenseItemDTO>();

                foreach (var item in order.Value.OrderItems)
                {
                    expenseItems.Add(new CreateExpenseItemDTO(){ 
                        Description = $"Expense by order number {orderResponse.Message} for {item.ArticleName}",
                        Amount = (decimal)item.PricePerItem, 
                        ExpenseId = 0});
                    var setArticleQuantityCommand = new SetArticleQuantityCommand() { Id = item.ArticleId, Quantity = item.Quantity};
                    var setQuantityContent= JsonContent.Create(setArticleQuantityCommand);

                    var responseC = await _httpClient.PutAsync("api/v/Article/updateQuantity", setQuantityContent);


                }

                var expense = new CreateExpenseDTO()
                {
                    Date = order.Value.OrderDate,
                    Description = $"Expense by order number {orderResponse.Message}",
                    Amount = (decimal)order.Value.TotalAmount,

                    ReferenceId = int.Parse(order.Value.OrderDate.ToString("yyyyMMdd")),
                    Items = expenseItems,
                    //OrderId = int.Parse(responseA)
                    OrderId = int.Parse(orderResponse.Message)
                };


                CreateExpenseCommand expenseCommand = new CreateExpenseCommand() { Value = expense};

                var expenseContent = JsonContent.Create(expenseCommand);


                var responseB = await _httpClient.PostAsync("api/v/Expense", expenseContent);


                
                return orderResponse; 


              
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
