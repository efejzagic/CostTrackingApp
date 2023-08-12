using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;

        public AccountController(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;   
        }


        //[HttpPost] 
        //public async Task<ActionResult<AuthenticationResponse?>> Authenticate([FromBody] AuthenticationRequest request)
        //{
        //    var response = await _jwtTokenHandler.GenerateJwtToken3(request);
        //    if (response == null) return Unauthorized();
        //    return response;
        //}

        //[HttpGet]
        //[Route("Roles")]
        //public async Task<IEnumerable<string>> GetRoles (string accesToken)
        //{
        //    return await _jwtTokenHandler.GetRolesFromKeycloak(accesToken);
        //}


        [HttpPost]
        [Route("test")]
        public async Task<ActionResult<string?>> Authenticate2([FromBody] AuthenticationRequest request)
        {
            var response = await _jwtTokenHandler.GenerateTokenWithRolesAsync(request);
            if (response == null) return Unauthorized();
            return response;
        }
    }
}
