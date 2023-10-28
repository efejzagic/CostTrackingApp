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
            keycloakConfig = new KeycloakConfig();

        }

        public async Task<ResponseInfo.Entities.Response<TokenResponse>> Handle(LoginTokenCommand request, CancellationToken cancellationToken)
        {
           

            try
            {
                using (var client = new HttpClient())
                {
                    var tokenEndpointUrl = $"{keycloakConfig.BaseUrl}/realms/{keycloakConfig.Realm}/protocol/openid-connect/token";

                    var parameters = new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "client_id", keycloakConfig.ClientId },
                    { "client_secret", keycloakConfig.ClientSecret },
                    { "username", request.Model.Username },
                    { "password", request.Model.Password }
                };

                    var response = await client.PostAsync(tokenEndpointUrl, new FormUrlEncodedContent(parameters));
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var jwtToken = tokenHandler.ReadJwtToken(tokenResponse.AccessToken);


                        return new ResponseInfo.Entities.Response<TokenResponse>(tokenResponse); ;
                    }
                    else
                    {
                        throw new Exception($"Keycloak response: {responseContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during login: {ex.Message}");
            }

        }
    }
}
