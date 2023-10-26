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
using DotNetEnv;

namespace Auth.Application.Features.Auth.Queries
{
    public class GetAllRolesQuery : IRequest<PagedResponse<List<Role>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IKey Key { get; set; }
    }
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, PagedResponse<List<Role>>>
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly KeycloakConfig keycloakConfig;
        
        public GetAllRolesQueryHandler(IHttpContextAccessor httpContextAccessor, HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            Env.Load();
            keycloakConfig = new KeycloakConfig()
            {
                BaseUrl = Environment.GetEnvironmentVariable("keycloakUrl"),
                Realm = Environment.GetEnvironmentVariable("realm"),
                ClientNumber = Environment.GetEnvironmentVariable("clientNumber")
                
            };
        }

        public async Task<PagedResponse<List<Role>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            string token = GetTokenFromHeader();

            if (token == null || token.Length == 0) { return null; }

            string rolesUrl = $"{keycloakConfig.BaseUrl}/admin/realms/{keycloakConfig.Realm}/clients/{keycloakConfig.ClientNumber}/roles";
            HttpRequestMessage requestHttp = new HttpRequestMessage(HttpMethod.Get, rolesUrl);
            requestHttp.Headers.Add("Authorization", $"Bearer {token}");

            HttpResponseMessage response = await _httpClient.SendAsync(requestHttp);
            response.EnsureSuccessStatusCode();
            var validFilter = _mapper.Map<GetAuthParameter>(request);

            string content = await response.Content.ReadAsStringAsync();
            List<Role> roles = JsonConvert.DeserializeObject<List<Role>>(content);
            return new PagedResponse<List<Role>>(roles, validFilter.PageNumber, validFilter.PageSize);
        }

        private string GetTokenFromHeader()
        {
            var context = _httpContextAccessor.HttpContext;

            if (context != null)
            {
                var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                // Your logic here
                return token;
            }
            return null;
        }


    }
}
