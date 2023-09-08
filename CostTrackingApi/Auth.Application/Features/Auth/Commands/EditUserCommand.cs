using Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseInfo.Entities;

namespace Auth.Application.Features.Auth.Commands
{
    public partial class EditUserCommand : IRequest<ResponseInfo.Entities.Response<string>>
    {
        public CreateUserModel Model { get; set; }
    }
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, ResponseInfo.Entities.Response<string>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EditUserCommandHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseInfo.Entities.Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();
            var keycloakConfig = new KeycloakConfig()
            {
                BaseUrl = "https://lemur-5.cloud-iam.com",
                Realm = "cost-tracking-app",
                ClientId = "cost-tracking-client",
                ClientSecret = "O6qyJVLColeu3KnncWrk7NpTyDSvNJZN"
            };

            var user = new Dictionary<string, object>
            {
                { "Username", request.Model.Username },
                { "Email", request.Model.Email},
                { "firstName", request.Model.Name},
                { "lastName", request.Model.Surname},
                { "enabled", true },
                { "credentials", new List<object>
                    {
                        new Dictionary<string, object>
                        {
                            { "type", "Password" },
                            { "value", request.Model.Password },
                            { "temporary", false }
                        }
                    }
                }
            };

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var endpoint = $"{keycloakConfig.BaseUrl}/auth/admin/realms/{keycloakConfig.Realm}/users";
            //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", request.Model.accessToken);
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = new ResponseInfo.Entities.Response<string>();
            try
            {
                var responseResult = await httpClient.PutAsync(endpoint, content);
                response.Succeeded = true;
                response.StatusCode = 200;
                responseResult.EnsureSuccessStatusCode();

                var responseContent = await responseResult.Content.ReadAsStringAsync();

                // Deserialize the response content into the desired type
                response.Data = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseContent);
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.StatusCode = 500;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
