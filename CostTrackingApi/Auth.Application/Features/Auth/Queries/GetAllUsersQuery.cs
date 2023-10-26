using Auth.Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using ResponseInfo.Entities;
using Auth.Application.Parameters.Auth;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace Auth.Application.Features.Auth.Queries
{
    public class GetAllUsersQuery : IRequest<PagedResponse<List<KeycloakUser>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IKey Key { get; set; }
    }
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PagedResponse<List<KeycloakUser>>>
    {
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly KeycloakConfig keycloakConfig;
        public GetAllUsersQueryHandler(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            keycloakConfig = new KeycloakConfig()
            {
                BaseUrl = Environment.GetEnvironmentVariable("keycloakUrl"),
                Realm = Environment.GetEnvironmentVariable("realm"),
            };
        }

        public async Task<PagedResponse<List<KeycloakUser>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var accessToken = GetTokenFromHeader();
            if (accessToken == null || accessToken == "null" || accessToken.IsNullOrEmpty()) return null;

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.GetAsync($"{keycloakConfig.BaseUrl}/admin/realms/{keycloakConfig.Realm}/users");
            var responseBody = await response.Content.ReadAsStringAsync();

            try
            {
                var users = JsonConvert.DeserializeObject<List<KeycloakUser>>(responseBody);
                var validFilter = _mapper.Map<GetAuthParameter>(request);

                return new PagedResponse<List<KeycloakUser>>(users, validFilter.PageNumber, validFilter.PageSize);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing response: {ex.Message}");
            }
        }

        private string? GetTokenFromHeader()
        {
            var context = _httpContextAccessor.HttpContext;

            if (context != null)
            {
                var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                return token;
            }
            return null;
        }
    }
}
