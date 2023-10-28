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
    public class GetCurrentUserIdQuery : IRequest<ResponseInfo.Entities.Response<AuthenticationResponse>>
    {
        
    }

    public class GetCurrentUserIdQueryHandler : IRequestHandler<GetCurrentUserIdQuery, ResponseInfo.Entities.Response<AuthenticationResponse>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetCurrentUserIdQueryHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseInfo.Entities.Response<AuthenticationResponse>> Handle(GetCurrentUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return new ResponseInfo.Entities.Response<AuthenticationResponse>(userId);
        }
    }
}
