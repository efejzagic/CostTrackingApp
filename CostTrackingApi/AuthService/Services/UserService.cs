using AuthService.Models;
using System.Text;

namespace AuthService.Services
{
    public class UserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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

    }
}
