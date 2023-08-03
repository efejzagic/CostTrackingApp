using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Auth.WebAPI.Services;
using Auth.Domain.Entities;
using Auth.Application.Features.Auth.Queries;
using Auth.Application.Features.Auth.Commands;

namespace Auth.WebAPI.Controllers
{


    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {

        [HttpGet]
        [Route("Id")]
        public async Task<Application.Wrappers.Response<TokenResponse>> GetUserId()
        {
            //var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //return userId;

            var userId = await Mediator.Send(new GetUserByIdQuery());
            return userId;
        }

        [HttpGet]
        [Route("UserData")]
        public async Task<Application.Wrappers.Response<KeycloakUserData>> GetUserDataFromKeycloak()
        {
            //return Ok(_loginService.GetUserData());
            return await Mediator.Send(new GetUserDataQuery());
        }

        //[HttpGet]
        //[Route("Proba")]
        //public string Proba()
        //{

        //    // Use the user ID or other claims as needed
        //    return "PROSLO";
        //}

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<Application.Wrappers.Response<TokenResponse>> Login([FromBody] LoginTokenCommand command)
        {
            //try
            //{
                //response = await _loginService.LoginToken(model);
                return await Mediator.Send(command);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
            //return Ok(response.AccessToken);
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
