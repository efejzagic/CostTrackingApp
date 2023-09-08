using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Auth.WebAPI.Services;
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

namespace Auth.WebAPI.Controllers
{


    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {

        private readonly UserService _userService;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtTokenHandler _tokenHandler;
        private readonly ITokenBlacklistService _tokenService;

        public AuthController(UserService userService, HttpClient httpClient, IHttpContextAccessor httpContextAccessor, 
            JwtTokenHandler tokenHandler, ITokenBlacklistService tokenService)
        {
            _userService = userService;
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _tokenHandler = tokenHandler;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = _userService.GetAllUsers();
            return Ok(response);
        }

        [HttpGet]
        [Route("Id")]
        public async Task<Application.Wrappers.Response<AuthenticationResponse>> GetUserId()
        {
            //var UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //return UserId;

            var userId = await Mediator.Send(new GetUserByIdQuery());
            return userId;
        }

        [HttpGet]
        [Route("UserData")]
        public async Task<IActionResult> GetUserDataFromKeycloak()
        {
            //return Ok(_loginService.GetUserData());
            return Ok(await Mediator.Send(new GetUserDataQuery()));
        }

        //[HttpPost("logout")]
        //public IActionResult Logout()
        //{
        //    // Get the token from the request (you might need to customize this)
        //    //var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        //    //// Invalidate the token by adding it to the blacklist
        //    //_tokenService.AddToBlacklist(token);

        //    //return Ok("Logged out successfully.");
        //    var newSecurityKey = new SymmetricSecurityKey(Guid.NewGuid().ToByteArray());

        //    // Update JWT settings with the new key
        //    _jwtSettings.SecurityKey = newSecurityKey;

        //    // Typically, you would also handle token expiration and revocation here

        //    return Ok("Logged out successfully. Tokens are invalidated.");
        //}

        //[HttpGet]
        //[Route("Proba")]
        //public string Proba()
        //{

        //    // Use the user ID or other claims as needed
        //    return "PROSLO";
        //}

        //[HttpPost("login")]
        //[AllowAnonymous]
        //public async Task<Application.Wrappers.Response<TokenResponse>> Login([FromBody] LoginTokenCommand command)
        //{
        //    //try
        //    //{
        //        //response = await _loginService.LoginToken(model);
        //        return await Mediator.Send(command);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return BadRequest(ex.Message);
        //    //}
        //    //return Ok(response.AccessToken);
        //}

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<TokenResponse> Login(AuthenticationRequest request)
        {
            var response = await _tokenHandler.LoginToken(request);
            return response;

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


        [HttpGet("roles")]
        [AllowAnonymous]

        public async Task<IActionResult> GetAllRolesAsync()
        {
            string token = GetTokenFromHeader();

            if(token==null || token.Length==0) { return Unauthorized(); }

            string rolesUrl = $"https://lemur-5.cloud-iam.com/auth/admin/realms/cost-tracking-app/clients/17199bf4-657f-458d-92f5-ceb815451556/roles";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, rolesUrl);
            request.Headers.Add("Authorization", $"Bearer {token}");

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            List<Role> roles = JsonConvert.DeserializeObject<List<Role>>(content);
            return Ok(roles);
        }


        [HttpPost("CreateUser")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            //return await _userService.CreateUser(model);
            var response = await Mediator.Send(command);
            return Ok(response);
        }

      

        [HttpPut("EditUser")]
        [AllowAnonymous]
        public async Task<IActionResult> EditUser(EditUserCommand command)
        {
            //return await _userService.EditUser(model);
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
