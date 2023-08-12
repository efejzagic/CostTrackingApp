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

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "O6qyJVLColeu3KnncWrk7NpTyDSvNJZN";
        public const int JWT_TOKEN_VALIDITY_MINS = 20;
        private readonly List<UserAccount> _userAccounts;

        public JwtTokenHandler()
        {
          
        }

        private static HttpClient _httpClient = new HttpClient();


        public async Task<string> GenerateTokenWithRolesAsync(AuthenticationRequest request)
        {
            string keycloakUrl = "https://lemur-5.cloud-iam.com/auth";
            string realm = "cost-tracking-app";

            // Configure client credentials
            string clientId = "cost-tracking-client";
            string clientSecret = "O6qyJVLColeu3KnncWrk7NpTyDSvNJZN";

            var tokenEndpoint = $"{keycloakUrl}/realms/{realm}/protocol/openid-connect/token";

            var tokenResponse = await _httpClient.PostAsync(tokenEndpoint, new FormUrlEncodedContent(new Dictionary<string, string>
{
    { "grant_type", "password" },
    { "client_id", clientId },
    { "client_secret", clientSecret },
    { "username", request.Username },
    { "password", request.Password },
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
                    return tokenHandler.WriteToken(token);


                }
                else
                {
                    
                    var errorContent = await userInfoResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"UserInfo Error: {errorContent}");
                    
                }
            }

            return null;
        }

       
        public AuthenticationResponse? GenerateJwtToken (AuthenticationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return null;
            //validation
            var userAccount = _userAccounts.Where(u => u.Username == request.Username && u.Password == request.Password).FirstOrDefault();
            if(userAccount == null) return null;

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, request.Username),
                new Claim(ClaimTypes.Role, userAccount.Role)
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                Username = userAccount.Username,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token
            };

        }


    }
}
