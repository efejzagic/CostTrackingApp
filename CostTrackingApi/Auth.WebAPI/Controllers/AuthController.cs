using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Auth.Domain.Entities;
using Auth.Application.Features.Auth.Queries;
using Auth.Application.Features.Auth.Commands;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using MediatR;
using System.Data;
using System.Runtime.InteropServices;
using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using TokenResponse = Auth.Domain.Entities.TokenResponse;
using Microsoft.IdentityModel.Tokens;
using ResponseInfo.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Auth.WebAPI.Controllers
{


    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {

        

        private readonly JwtTokenHandler _tokenHandler;

        public AuthController(
            JwtTokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
        }


        [HttpGet("GetUserById/{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(string userId)
        {
            return Ok(await Mediator.Send(new GetUserByIdQuery { Id = userId }));

        }


        [HttpGet]
        [Route("Id")]
        public async Task<ResponseInfo.Entities.Response<AuthenticationResponse>> GetUserId()
        {
            var userId = await Mediator.Send(new GetCurrentUserIdQuery());
            return userId;
        }

        [HttpGet]
        [Route("UserData")]
        public async Task<IActionResult> GetUserDataFromKeycloak()
        {
            return Ok(await Mediator.Send(new GetUserDataQuery()));
        }

       

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<TokenResponse> Login(AuthenticationRequest request)
        {
            var response = await _tokenHandler.LoginToken(request);
            return response;
        }


       

        [HttpPost("CreateUser")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

      

        [HttpPut("EditUser")]
        [AllowAnonymous]
        public async Task<IActionResult> EditUser(EditUserCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

       
    }
}
