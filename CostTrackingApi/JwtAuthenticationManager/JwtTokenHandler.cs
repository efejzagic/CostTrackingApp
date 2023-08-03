using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "O6qyJVLColeu3KnncWrk7NpTyDSvNJZN";
        public const int JWT_TOKEN_VALIDITY_MINS = 20;
        private readonly List<UserAccount> _userAccounts;

        public JwtTokenHandler()
        {
            _userAccounts = new List<UserAccount>
            {
                new UserAccount{Username = "admin", Password = "password", Role = "Administrator"},
                new UserAccount{Username = "user", Password = "User", Role = "User"}
            };
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
