using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Auth.Application.Features.Auth.Queries
{
    public class GetUserDataQuery : IRequest<ResponseInfo.Entities.Response<KeycloakUserData>>
    {
        public string Id { get; set; }
    }

    public class GetUserDataQueryHandler : IRequestHandler<GetUserDataQuery, ResponseInfo.Entities.Response<KeycloakUserData>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserDataQueryHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseInfo.Entities.Response<KeycloakUserData>> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var name = _httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == "Name")?.Value;
            var roles = _httpContextAccessor.HttpContext.User.Claims
                .Where(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                .Select(claim => claim.Value)
                .ToList();
            var userData = new KeycloakUserData
            {
                Id = userId,
                Email = email,
                Name = name, 
                Roles = roles
            };

            return new ResponseInfo.Entities.Response<KeycloakUserData>(userData);

        }


    }


}
