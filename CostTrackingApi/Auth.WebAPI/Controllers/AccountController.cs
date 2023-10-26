using Auth.Domain.Entities;
using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Auth.Application.Features.Auth.Queries;
using Auth.Application.Parameters.Auth;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Auth.WebAPI.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly JwtTokenHandler _jwtTokenHandler;
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(JwtTokenHandler jwtTokenHandler, IHttpClientFactory httpClientFactory)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _httpClientFactory = httpClientFactory;
        }
        [Authorize]
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            return Ok("Test");
        }

        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAuthParameter filter)
        {

            return Ok(await Mediator.Send(new GetAllUsersQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));

            //try
            //{


            //    //var response = await _jwtTokenHandler.GetAllUsers();
            //    if (response == null) return Unauthorized();
            //    return Ok(response);
            //}
            //catch (Exception ex)
            //{
            //    return Unauthorized();
        
        }
        [HttpGet("roles")]
        [AllowAnonymous]

        public async Task<IActionResult> GetAllRolesAsync([FromQuery] GetAuthParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllRolesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));

        }



    }
}
