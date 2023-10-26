using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auth.Domain.Entities;
using DotNetEnv;
using JwtAuthenticationManager.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Auth.Application.Features.Auth.Queries
{
    public class GetUserByIdQuery : IRequest<ResponseInfo.Entities.Response<KeycloakUser>>
    {
        public string Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResponseInfo.Entities.Response<KeycloakUser>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly KeycloakConfig keycloakConfig;
        public GetUserByIdQueryHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            Env.Load();
            keycloakConfig = new KeycloakConfig()
            {
                BaseUrl = Environment.GetEnvironmentVariable("keycloakUrl"),
                Realm = Environment.GetEnvironmentVariable("realm")
            };
        }

        public async Task<ResponseInfo.Entities.Response<KeycloakUser>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            //var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //return new ResponseInfo.Entities.Response<AuthenticationResponse>(userId);

            var httpClient = new HttpClient();
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            if (string.IsNullOrEmpty(request.Id) || string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException();
            }

            // Define the endpoint to get a user by ID
            var endpoint = $"{keycloakConfig.BaseUrl}/admin/realms/{keycloakConfig.Realm}/users/{request.Id}";

            try
            {
                var response = await httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    KeycloakUser user = JsonConvert.DeserializeObject<KeycloakUser>(responseString);

                    //var responseJson = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
                    return new ResponseInfo.Entities.Response<KeycloakUser>(user);
                }
                else
                {
                    // Handle error cases
                    throw new Exception($"Failed to get user by ID. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw ex;
            }
        }


    }


}
