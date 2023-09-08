using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auth.Domain.Entities;
using JwtAuthenticationManager.Models;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace Auth.Application.Features.Auth.Queries
{
    public class GetUserByIdQuery : IRequest<ResponseInfo.Entities.Response<AuthenticationResponse>>
    {
        public string Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResponseInfo.Entities.Response<AuthenticationResponse>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserByIdQueryHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseInfo.Entities.Response<AuthenticationResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return new ResponseInfo.Entities.Response<AuthenticationResponse>(userId);
        }


    }


}
