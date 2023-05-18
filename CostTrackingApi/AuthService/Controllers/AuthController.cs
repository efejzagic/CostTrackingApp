using AuthService.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using AuthService.Services;

namespace AuthService.Controllers
{


    [ApiController]
    [Authorize]
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
        [Route("All")]
        public async Task<List<User>> GetAllUsers()
        {
            var accessToken = _httpContextAccessor.HttpContext.GetTokenAsync("access_token").Result;
            var requestUrl = "https://your-keycloak-server/auth/admin/realms/your-realm/users";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<List<User>>(responseContent);
                    return users;
                }
                else
                {
                    // Handle error response
                    // For example, log the error or throw an exception
                    // throw new Exception("Failed to retrieve users");
                }
            }

            return null;
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
