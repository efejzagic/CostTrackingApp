using Auth.Domain.Entities;
using System.Net.Http.Headers;
using System.Net.Http;

using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Auth.WebAPI.Services
{
    public class UserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IEnumerable<KeycloakUser>> GetAllUsers()
        {
            var httpClient = new HttpClient();


            var keycloakConfig = new KeycloakConfig()
            {
                BaseUrl = "https://lemur-5.cloud-iam.com",
                Realm = "cost-tracking-app",
                ClientId = "cost-tracking-client",
                ClientSecret = "O6qyJVLColeu3KnncWrk7NpTyDSvNJZN"
            };

            var accessToken = await GetAccessToken();

            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //var response = await httpClient.GetAsync($"{keycloakConfig.BaseUrl}/auth/admin/realms/{keycloakConfig.Realm}/users");

            //var endpoint = $"{keycloakConfig.BaseUrl}/auth/admin/realms/{keycloakConfig.Realm}/users";
            //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            //var response = await httpClient.GetAsync(endpoint);

            // Set the authorization header with the access token
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Make the request to get all users
            var response = await httpClient.GetAsync($"{keycloakConfig.BaseUrl}/auth/admin/realms/{keycloakConfig.Realm}/users");
            //response.EnsureSuccessStatusCode();

            //if (response.IsSuccessStatusCode)
            //{
            //    var userJsonResponse = await response.Content.ReadAsStringAsync();
            //    return (IEnumerable<KeycloakUser>)JsonSerializer.Deserialize<KeycloakUser>(userJsonResponse);
            //}


            //// Handle error response if needed
            //return null;

            response.EnsureSuccessStatusCode();

            // Parse the response JSON into a list of KeycloakUser
            var content = await response.Content.ReadAsStringAsync();
            var users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<KeycloakUser>>(content);

            return users;
        }
        private async Task<string> GetAccessToken()
        {
            
            var keycloakConfig = new KeycloakConfig()
            {
                BaseUrl = "https://lemur-5.cloud-iam.com",
                Realm = "cost-tracking-app",
                ClientId = "cost-tracking-client",
                ClientSecret = "O6qyJVLColeu3KnncWrk7NpTyDSvNJZN"
            };

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(keycloakConfig.BaseUrl)
            };


            var tokenEndpoint = $"/auth/realms/{keycloakConfig.Realm}/protocol/openid-connect/token";
            var clientIdAndSecret = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{keycloakConfig.ClientId}:{keycloakConfig.ClientSecret}"));

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" },
             { "client_id", keycloakConfig.ClientId },
            { "client_secret", keycloakConfig.ClientSecret },
                { "scope", "openid" } // Add the openid scope here

        });

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", clientIdAndSecret);
            var response = await httpClient.PostAsync(tokenEndpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var tokenJson = await response.Content.ReadAsStringAsync();
                var tokenObj = JsonSerializer.Deserialize<JsonElement>(tokenJson);
                return tokenObj.GetProperty("access_token").GetString();
            }

            // Handle error response if needed
            return null;
        }
        public async Task<Response<string>> CreateUser(CreateUserModel model)
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
                { "username", model.username },
                { "email", model.email},
                { "firstName", model.name},
                { "lastName", model.surname},
                { "enabled", true },
                { "credentials", new List<object>
                    {
                        new Dictionary<string, object>
                        {
                            { "type", "password" },
                            { "value", model.password },
                            { "temporary", false }
                        }
                    }
                }
            };

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var endpoint = $"{keycloakConfig.BaseUrl}/auth/admin/realms/{keycloakConfig.Realm}/users";
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", model.accessToken);

            var response = new Response<string>();
            try
            {
                var responseResult = await httpClient.PostAsync(endpoint, content);
                response.Success = true;
                response.StatusCode = 200;
                responseResult.EnsureSuccessStatusCode();

                var responseContent = await responseResult.Content.ReadAsStringAsync();

                // Deserialize the response content into the desired type
                response.Data = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseContent);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = 500;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }


        public async Task<Response<string>> EditUser(CreateUserModel model)
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
                { "username", model.username },
                { "email", model.email},
                { "firstName", model.name},
                { "lastName", model.surname},
                { "enabled", true },
                { "credentials", new List<object>
                    {
                        new Dictionary<string, object>
                        {
                            { "type", "password" },
                            { "value", model.password },
                            { "temporary", false }
                        }
                    }
                }
            };

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var endpoint = $"{keycloakConfig.BaseUrl}/auth/admin/realms/{keycloakConfig.Realm}/users/{model.userId}";
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", model.accessToken);

            var response = new Response<string>();
            try
            {
                var responseResult = await httpClient.PutAsync(endpoint, content);
                response.Success = true;
                response.StatusCode = 200;
                responseResult.EnsureSuccessStatusCode();

                var responseContent = await responseResult.Content.ReadAsStringAsync();

                // Deserialize the response content into the desired type
                response.Data = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseContent);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = 500;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }


    }
}
