using Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ResponseInfo.Entities;
using DotNetEnv;

namespace Auth.Application.Features.Auth.Commands
{
    public partial class CreateUserCommand : IRequest<ResponseInfo.Entities.Response<string>>
    {
        public CreateUserModel Model { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseInfo.Entities.Response<string>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private KeycloakConfig keycloakConfig;
        public CreateUserCommandHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            keycloakConfig = new KeycloakConfig();
        }

        public async Task<ResponseInfo.Entities.Response<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();

            var test = keycloakConfig;
            var user = new Dictionary<string, object>
            {
                { "username", request.Model.Username },
                { "email", request.Model.Email},
                { "firstName", request.Model.Name},
                { "lastName", request.Model.Surname},
                { "enabled", true },
                { "emailVerified", true }, 
                { "credentials", new List<object>
                    {
                        new Dictionary<string, object>
                        {
                            { "type", "password" },
                            { "value", request.Model.Password },
                            { "temporary", false}
                        }
                    }
                },
            };

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var endpoint = $"{keycloakConfig.BaseUrl}/admin/realms/{keycloakConfig.Realm}/users";
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = new ResponseInfo.Entities.Response<string>();
            try
            {
                var responseResult = await httpClient.PostAsync(endpoint, content);
                response.Succeeded = true;
                response.StatusCode = 200;
                responseResult.EnsureSuccessStatusCode();

                var responseContent = await responseResult.Content.ReadAsStringAsync();
                response.Data = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseContent);
                var userId = await GetUserIdByUsername(request.Model.Username);
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.StatusCode = 500;
                response.Message = ex.Message;
            }


            return response;
        }

        private async Task<bool> AssignRolesToUser(string userId, List<Role> MultipleRoles)
        {
            
            try
            {
                string userIdEncoded = Uri.EscapeDataString(userId);

                using var httpClient = new HttpClient();
                var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                var rolePayloads = new List<object>();


                foreach (var temp in MultipleRoles)
                {
                    var rolePayload = new
                    {
                        id = temp.Id,
                        name = temp.Name
                    };

                    rolePayloads.Add(rolePayload);
                }

                var jsonPayload = JsonConvert.SerializeObject(rolePayloads);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{keycloakConfig.BaseUrl}/admin/realms/{keycloakConfig.Realm}/users/{userIdEncoded}/role-mappings/clients/17199bf4-657f-458d-92f5-ceb815451556", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Failed to map role  to user with ID '{userId}'.");
                        Console.WriteLine($"Response: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                        return response.IsSuccessStatusCode;                    }
                    else
                    {
                        Console.WriteLine($"Successfully mapped role to user with ID '{userId}'.");
                        return response.IsSuccessStatusCode;
                    }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return false;

        }

        public async Task<string> GetUserIdByUsername (string username)
        {


            string usernameToSearch = username; 

            var httpClient = new HttpClient();
            var endpoint = $"{keycloakConfig.BaseUrl}/admin/realms/{keycloakConfig.Realm}/users";
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            
            string searchEndpoint = $"{keycloakConfig.BaseUrl}/admin/realms/{keycloakConfig.Realm}/users";
            string query = $"?username={usernameToSearch}";

            var response = await httpClient.GetAsync(searchEndpoint + query);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<KeycloakUser>>(responseContent);

                if (users.Count > 0)
                {
                    string userId = users[0].id; 
                    return userId;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
    }
}
