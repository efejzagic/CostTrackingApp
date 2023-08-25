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

namespace Auth.Application.Features.Auth.Commands
{
    public partial class CreateUserCommand : IRequest<Wrappers.Response<string>>
    {
        public CreateUserModel Model { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Wrappers.Response<string>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateUserCommandHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Wrappers.Response<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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
                { "username", request.Model.Username },
                { "email", request.Model.Email},
                { "firstName", request.Model.Name},
                { "lastName", request.Model.Surname},
                { "enabled", true },
                { "credentials", new List<object>
                    {
                        new Dictionary<string, object>
                        {
                            { "type", "password" },
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
            //var responseResult = await httpClient.PostAsync(endpoint, content);

            var response = new Application.Wrappers.Response<string>();
            try
            {
                var responseResult = await httpClient.PostAsync(endpoint, content);
                response.Succeeded = true;
                response.StatusCode = 200;
                responseResult.EnsureSuccessStatusCode();

                var responseContent = await responseResult.Content.ReadAsStringAsync();

                // Deserialize the response content into the desired type
                response.Data = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseContent);
                var userId = await GetUserIdByUsername(request.Model.Username);
                AssignRolesToUser(userId, request.Model.MultipleRoles);
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.StatusCode = 500;
                response.Message = ex.Message;
            }


            //return response;
            return response;
            //return new Response<string>(enviroment.Id.ToString());
        }

        private async Task<bool> AssignRolesToUser(string userId, List<Role> MultipleRoles)
        {
            //var httpClient = new HttpClient();

            var keycloakConfig = new KeycloakConfig()
            {
                BaseUrl = "https://lemur-5.cloud-iam.com",
                Realm = "cost-tracking-app",
                ClientId = "cost-tracking-client",
                ClientSecret = "O6qyJVLColeu3KnncWrk7NpTyDSvNJZN"
            };

            //var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);


            //var assignRolesEndpoint = $"{keycloakConfig.BaseUrl}/auth/admin/realms/{keycloakConfig.Realm}/users/{UserId}/role-mappings/clients/17199bf4-657f-458d-92f5-ceb815451556";

            //var rolesToAssign = MultipleRoles.Select(role => role.Id);

            //var rolesJson = JsonConvert.SerializeObject(rolesToAssign);
            //var content = new StringContent(rolesJson, Encoding.UTF8, "application/json");

            //var response = await httpClient.PostAsync(assignRolesEndpoint, content);

            //return response.IsSuccessStatusCode;


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

                var response = await httpClient.PostAsync($"{keycloakConfig.BaseUrl}/auth/admin/realms/{keycloakConfig.Realm}/users/{userIdEncoded}/role-mappings/clients/17199bf4-657f-458d-92f5-ceb815451556", content);

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

            var keycloakConfig = new KeycloakConfig()
            {
                BaseUrl = "https://lemur-5.cloud-iam.com",
                Realm = "cost-tracking-app",
                ClientId = "cost-tracking-client",
                ClientSecret = "O6qyJVLColeu3KnncWrk7NpTyDSvNJZN"
            };

            string usernameToSearch = username; // Replace with the Username you want to search

            var httpClient = new HttpClient();
            var endpoint = $"{keycloakConfig.BaseUrl}/auth/admin/realms/{keycloakConfig.Realm}/users";
            //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", request.Model.accessToken);
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            //var responseResult = await httpClient.PostAsync(endpoint, content);
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string searchEndpoint = $"{keycloakConfig.BaseUrl}/auth/admin/realms/{keycloakConfig.Realm}/users";
            string query = $"?username={usernameToSearch}";

            var response = await httpClient.GetAsync(searchEndpoint + query);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<KeycloakUser>>(responseContent);

                if (users.Count > 0)
                {
                    string userId = users[0].id; // Assuming UserModel has a property named "Id"
                    return userId;
                    Console.WriteLine($"User ID for Username '{usernameToSearch}': {userId}");
                }
                else
                {
                    Console.WriteLine($"No user found with Username '{usernameToSearch}'.");
                    return null;
                }
            }
            else
            {
                Console.WriteLine($"Error searching for user. StatusCode: {response.StatusCode}");
                return null;
            }
            return null;

        }
    }
}
