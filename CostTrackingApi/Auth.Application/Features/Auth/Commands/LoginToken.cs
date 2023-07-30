using Auth.Application.Wrappers;
using Auth.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Features.Auth.Commands
{
    public partial class LoginToken : IRequest<Wrappers.Response<TokenResponse>>
    {
        public LoginRequest Model { get; set; }
    }
    public class LoginRequestHandler : IRequestHandler<LoginToken, Wrappers.Response<TokenResponse>>
    {
        public LoginRequestHandler()
        {
        }

        public async Task<Wrappers.Response<TokenResponse>> Handle(LoginToken request, CancellationToken cancellationToken)
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
                        return new Wrappers.Response<TokenResponse>(tokenResponse); ;
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

            //return new Response<string>(enviroment.Id.ToString());
        }
    }
}
