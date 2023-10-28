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


        public AccountController(){ }

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
        }
        [HttpGet("roles")]
        [AllowAnonymous]

        public async Task<IActionResult> GetAllRolesAsync([FromQuery] GetAuthParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllRolesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));

        }



    }
}
