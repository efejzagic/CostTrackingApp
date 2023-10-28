using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Auth.Domain.Entities;
using Microsoft.AspNetCore.Http;
using DotNetEnv;
using static System.Net.WebRequestMethods;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const int JWT_TOKEN_VALIDITY_MINS = 20;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private string keycloakUrl = "https://lemur-5.cloud-iam.com/auth";
        private string clientId = "cost-tracking-client";
        private string realm = "cost-tracking-app";
        private string clientSecret = "O6qyJVLColeu3KnncWrk7NpTyDSvNJZN";

        public JwtTokenHandler(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;


        }

        private static HttpClient _httpClient = new HttpClient();

        public async Task<Auth.Domain.Entities.TokenResponse?> LoginToken(AuthenticationRequest model)
        { 


            try
            {
                using (var client = new HttpClient())
                {
                    var tokenEndpointUrl = $"{keycloakUrl}/realms/{realm}/protocol/openid-connect/token";

                    var parameters = new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "client_id", clientId },
                    { "client_secret", clientSecret },
                    { "username", model.Username },
                    { "password", model.Password }
                };

                    var response = await client.PostAsync(tokenEndpointUrl, new FormUrlEncodedContent(parameters));
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var tokenResponse = JsonConvert.DeserializeObject<Auth.Domain.Entities.TokenResponse>(responseContent);

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var jwtToken = tokenHandler.ReadJwtToken(tokenResponse.AccessToken);

                     
                        return tokenResponse;
                    }
                    else
                    {
                        throw new Exception($"Keycloak response: {responseContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during login: {ex.Message}");
            }

        }


        public async Task<AuthenticationResponse> GenerateTokenWithRolesAsync(AuthenticationRequest request)
        {


            var tokenEndpoint = $"{keycloakUrl}/realms/{realm}/protocol/openid-connect/token";

            var tokenResponse = await _httpClient.PostAsync(tokenEndpoint, new FormUrlEncodedContent(new Dictionary<string, string>
{
    { "grant_type", "Password" },
    { "client_id", clientId },
    { "client_secret", clientSecret },
    { "Username", request.Username },
    { "Password", request.Password },
    { "scope", "openid" } 
}));

            if (tokenResponse.IsSuccessStatusCode)
            {
                var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
                var tokenData = JObject.Parse(tokenContent);
                var accessToken = tokenData["access_token"].ToString();

                var userInfoEndpoint = $"{keycloakUrl}/realms/{realm}/protocol/openid-connect/userinfo";
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var userInfoResponse = await _httpClient.GetAsync(userInfoEndpoint);

                if (userInfoResponse.IsSuccessStatusCode)
                {
                    var userInfoContent = await userInfoResponse.Content.ReadAsStringAsync();
                    var userInfo = JObject.Parse(userInfoContent);
                    var roles = userInfo["roles"].ToObject<List<string>>();

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clientSecret));

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, request.Username),
                        new Claim(ClaimTypes.Role, "realmRole") 
                    };

                    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.UtcNow.AddHours(1), 
                        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var responseToken = tokenHandler.WriteToken(token);

                    var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);

                    return new AuthenticationResponse()
                    {
                        Username = request.Username,
                        JwtToken = responseToken,
                        ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                        Roles = roles
                    };

                }
                else
                {

                    var errorContent = await userInfoResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"UserInfo Error: {errorContent}");

                }
            }

            return null;
        }

        private string GetTokenFromHeader()
        {
            var context = _httpContextAccessor.HttpContext;

            if (context != null)
            {
                var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                return token;
            }
            return null;
        }

        public async Task<List<KeycloakUser>> GetAllUsers()
        {

            var accessToken = GetTokenFromHeader();
            if (accessToken == null || accessToken == "null" || accessToken.IsNullOrEmpty()) return null;

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.GetAsync("https://lemur-10.cloud-iam.com/auth/admin/realms/df-app/users");
            var responseBody = await response.Content.ReadAsStringAsync();

            try
            {
                var users = JsonConvert.DeserializeObject<List<KeycloakUser>>(responseBody);

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing response: {ex.Message}");
            }
        }



        public async Task<bool> Logout(string accessToken)
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string logoutUrl = $"{keycloakUrl}/realms/{realm}/protocol/openid-connect/logout?id_token_hint={accessToken}";

                    HttpResponseMessage response = await client.PostAsync(logoutUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Token invalidated successfully.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Token invalidation failed.");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            return false;
        }



    }
}
