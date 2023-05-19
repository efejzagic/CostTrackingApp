using AuthService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AuthService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace AuthService.Controllers
{


    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoginService _loginService;


        public AuthController(IHttpContextAccessor httpContextAccessor, LoginService loginService)
        {
            _httpContextAccessor = httpContextAccessor;
            _loginService = loginService;
        }

        

        [HttpGet]
        [Route("Id")]
        public string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // Use the user ID or other claims as needed
            return userId;
        }

        [HttpGet]
        [Route("UserData")]
        public async Task<IActionResult> GetUserDataFromKeycloak()
        {
            return Ok(_loginService.GetUserData());
        }

        [HttpGet]
        [Route("Proba")]
        public string Proba()
        {
            
            // Use the user ID or other claims as needed
            return "PROSLO";
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            TokenResponse response;
            try
            {
                response = await _loginService.LoginToken(model);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response.AccessToken);
        }
    }
}
