﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auth.Application.Wrappers;
using Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Auth.Application.Features.Auth.Queries
{
    public class GetUserDataQuery : IRequest<Wrappers.Response<KeycloakUserData>>
    {
        public string Id { get; set; }
    }

    public class GetUserDataQueryHandler : IRequestHandler<GetUserDataQuery, Wrappers.Response<KeycloakUserData>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserDataQueryHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Wrappers.Response<KeycloakUserData>> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
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

            return new Wrappers.Response<KeycloakUserData>(userData);

        }


    }


}