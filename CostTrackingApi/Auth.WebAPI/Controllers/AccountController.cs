using Auth.Domain.Entities;
using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using MediatR;

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
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Ok("Test");
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
        //[HttpPost("logout")]
        //public async Task<bool> Authenticate2()
        //{
        //    var response = await _jwtTokenHandler.Logout();
        //    return response;
        //}
        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var response = await _jwtTokenHandler.GetAllUsers();
                if (response == null) return Unauthorized();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
            //var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            //var httpClient = _httpClientFactory.CreateClient();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            //var response = await httpClient.GetAsync("https://lemur-5.cloud-iam.com/auth/admin/realms/cost-tracking-app/users");
            //var responseBody = await response.Content.ReadAsStringAsync();

            //// Process the responseBody to extract user data
            //try
            //{
            //    // Deserialize the response JSON into a list of user objects
            //    var users = JsonConvert.DeserializeObject<List<KeycloakUser>>(responseBody);

            //    // Do something with the list of users
            //    return Ok(users);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest($"Error processing response: {ex.Message}");
            //}
        }

        [HttpPost("invalidate-token")]
        public async Task<IActionResult> InvalidateToken(string sessionID)
        {
            // Use the session ID to invalidate the session in Keycloak
            if (!string.IsNullOrEmpty(sessionID))
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var keycloakBaseUrl = "https://your-keycloak-domain/auth";
                    var adminRealm = "master"; // The admin realm
                    var adminUsername = "admin"; // The admin Username
                    var adminPassword = "admin-Password"; // The admin Password

                    var tokenUrl = $"{keycloakBaseUrl}/realms/{adminRealm}/protocol/openid-connect/token";
                    var invalidateUrl = $"{keycloakBaseUrl}/admin/realms/{adminRealm}/sessions/{sessionID}/logout";

                    var tokenRequestContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", "Password"),
                        new KeyValuePair<string, string>("Username", adminUsername),
                        new KeyValuePair<string, string>("Password", adminPassword),
                        new KeyValuePair<string, string>("client_id", "admin-cli")
                    });

                    var tokenResponse = await httpClient.PostAsync(tokenUrl, tokenRequestContent);
                    var tokenResponseContent = await tokenResponse.Content.ReadAsStringAsync();

                    // Parse the access token from the response (ensure proper error handling here)
                    var accessToken = "extracted-access-token"; // Extract the access token from the response

                    // Use the access token to call the invalidate session endpoint
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    var invalidateResponse = await httpClient.PostAsync(invalidateUrl, null);

                    if (invalidateResponse.IsSuccessStatusCode)
                    {
                        return Ok(new { message = "Token invalidated successfully." });
                    }
                    else
                    {
                        return BadRequest(new { message = "Failed to invalidate the token." });
                    }
                }
            }
            else
            {
                return BadRequest(new { message = "Invalid session ID." });
            }
        }


        [HttpPost("test")]
        public async Task<ActionResult<AuthenticationResponse?>> Authenticate2([FromBody] AuthenticationRequest request)
        {
            var response = await _jwtTokenHandler.GenerateTokenWithRolesAsync(request);
            if (response == null) return Unauthorized();
            return response;
        }
    }
}
