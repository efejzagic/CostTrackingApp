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

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "O6qyJVLColeu3KnncWrk7NpTyDSvNJZN";
        public const int JWT_TOKEN_VALIDITY_MINS = 20;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtTokenHandler(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;

        }

        private static HttpClient _httpClient = new HttpClient();


        public async Task<AuthenticationResponse> GenerateTokenWithRolesAsync(AuthenticationRequest request)
        {
            string keycloakUrl = "https://lemur-5.cloud-iam.com/auth";
            string realm = "cost-tracking-app";

            // Configure client credentials
            string clientId = "cost-tracking-client";
            string clientSecret = "O6qyJVLColeu3KnncWrk7NpTyDSvNJZN";

            var tokenEndpoint = $"{keycloakUrl}/realms/{realm}/protocol/openid-connect/token";

            var tokenResponse = await _httpClient.PostAsync(tokenEndpoint, new FormUrlEncodedContent(new Dictionary<string, string>
{
    { "grant_type", "Password" },
    { "client_id", clientId },
    { "client_secret", clientSecret },
    { "Username", request.Username },
    { "Password", request.Password },
    { "scope", "openid" } // Add the openid scope here
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
                        new Claim(ClaimTypes.Role, "realmRole") // Add realm role
                    };

                    // Add additional roles from userinfo endpoint
                    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.UtcNow.AddHours(1), // Set token expiration
                        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var responseToken =  tokenHandler.WriteToken(token);

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
                // Your logic here
                return token;
            }
            return null;
        }

        public async Task<List<KeycloakUser>> GetAllUsers ()
        {

            var accessToken = GetTokenFromHeader();
            if (accessToken == null || accessToken=="null" || accessToken.IsNullOrEmpty()) return null;

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.GetAsync("https://lemur-5.cloud-iam.com/auth/admin/realms/cost-tracking-app/users");
            var responseBody = await response.Content.ReadAsStringAsync();

            // Process the responseBody to extract user data
            try
            {
                // Deserialize the response JSON into a list of user objects
                var users = JsonConvert.DeserializeObject<List<KeycloakUser>>(responseBody);

                // Do something with the list of users
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing response: {ex.Message}");
            }
        }



       public async Task<bool> Logout (string accessToken)
        {
            string keycloakUrl = "https://lemur-5.cloud-iam.com/auth";
            string realm = "cost-tracking-app";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Construct the logout URL
                    string logoutUrl = $"{keycloakUrl}/realms/{realm}/protocol/openid-connect/logout?id_token_hint={accessToken}";

                    // Make the request to the Keycloak logout endpoint
                    HttpResponseMessage response = await client.PostAsync(logoutUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Token invalidated successfully.");
                        // Clear the locally stored token(s)
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Token invalidation failed.");
                        // Handle the failure scenario
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    // Handle the exception
                }
            }
            return false;
        }

        //public AuthenticationResponse? GenerateJwtToken (AuthenticationRequest request)
        //{
        //    if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        //        return null;
        //    //validation
        //    var userAccount = _userAccounts.Where(u => u.Username == request.Username && u.Password == request.Password).FirstOrDefault();
        //    if(userAccount == null) return null;

        //    var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
        //    var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
        //    var claimsIdentity = new ClaimsIdentity(new List<Claim>
        //    {
        //        new Claim(JwtRegisteredClaimNames.Name, request.Username),
        //        new Claim(ClaimTypes.Role, userAccount.Role)
        //    });

        //    var signingCredentials = new SigningCredentials(
        //        new SymmetricSecurityKey(tokenKey),
        //        SecurityAlgorithms.HmacSha256Signature);
        //    var securityTokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = claimsIdentity,
        //        Expires = tokenExpiryTimeStamp,
        //        SigningCredentials = signingCredentials
        //    };

        //    var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        //    var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        //    var token = jwtSecurityTokenHandler.WriteToken(securityToken);

        //    return new AuthenticationResponse
        //    {
        //        Username = userAccount.Username,
        //        ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
        //        JwtToken = token
        //    };

        //}


    }
}
