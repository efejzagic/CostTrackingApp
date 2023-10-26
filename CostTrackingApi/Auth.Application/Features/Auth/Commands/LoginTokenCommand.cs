using Auth.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Features.Auth.Commands
{
    public partial class LoginTokenCommand : IRequest<ResponseInfo.Entities.Response<TokenResponse>>
    {
        public LoginRequest Model { get; set; }
    }
    public class LoginRequestCommandHandler : IRequestHandler<LoginTokenCommand, ResponseInfo.Entities.Response<TokenResponse>>
    {
        private KeycloakConfig keycloakConfig;
        public LoginRequestCommandHandler()
        {
            keycloakConfig = new KeycloakConfig()
            {
                Realm = Environment.GetEnvironmentVariable("realm"),
                ClientId = Environment.GetEnvironmentVariable("clientId"),
                ClientSecret = Environment.GetEnvironmentVariable("clientSecret"),
                BaseUrl = Environment.GetEnvironmentVariable("keycloakUrl")
            };
        }

        public async Task<ResponseInfo.Entities.Response<TokenResponse>> Handle(LoginTokenCommand request, CancellationToken cancellationToken)
        {
           

            try
            {
                // Create the HTTP client
                using (var client = new HttpClient())
                {
                    // Prepare the token endpoint URL
                    var tokenEndpointUrl = $"{keycloakConfig.BaseUrl}/realms/{keycloakConfig.Realm}/protocol/openid-connect/token";

                    // Prepare the request body parameters
                    var parameters = new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "client_id", keycloakConfig.ClientId },
                    { "client_secret", keycloakConfig.ClientSecret },
                    { "username", request.Model.Username },
                    { "password", request.Model.Password }
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
                        return new ResponseInfo.Entities.Response<TokenResponse>(tokenResponse); ;
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
    }
}
