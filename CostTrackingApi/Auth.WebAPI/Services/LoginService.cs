using Auth.Domain.Entities;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Auth.WebAPI.Services
{
    public class LoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<TokenResponse> LoginToken(LoginRequest model)
        {
            string keycloakUrl = "https://lemur-5.cloud-iam.com/auth";
            string realm = "cost-tracking-app";

            // Configure client credentials
            string clientId = "cost-tracking-client";
            string clientSecret = "O6qyJVLColeu3KnncWrk7NpTyDSvNJZN";

            try
            {
                // Create the HTTP client
                using (var client = new HttpClient())
                {
                    // Prepare the token endpoint URL
                    var tokenEndpointUrl = $"{keycloakUrl}/realms/{realm}/protocol/openid-connect/token";

                    // Prepare the request body parameters
                    var parameters = new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "client_id", clientId },
                    { "client_secret", clientSecret },
                    { "username", model.Username },
                    { "password", model.Password }
                };

                    // Send the request to Keycloak token endpoint
                    var response = await client.PostAsync(tokenEndpointUrl, new FormUrlEncodedContent(parameters));
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Deserialize the response to a token response model
                        var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                        // Create the JWT token handler
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var jwtToken = tokenHandler.ReadJwtToken(tokenResponse.AccessToken);

                        // Optionally, you can perform additional checks or validations on the JWT token

                        // Return the JWT token as a response
                        return tokenResponse;
                    }
                    else
                    {
                        // Return the error response from Keycloak
                        throw new Exception($"Keycloak response: {responseContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the login process
                throw new Exception($"An error occurred during login: {ex.Message}");
            }
        }

        public async Task<KeycloakUserData> GetUserData()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var name = _httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == "name")?.Value;

            var userData = new KeycloakUserData
            {
                Id = userId,
                Email = email,
                Name = name
            };

            return userData;
        }
    }
}
