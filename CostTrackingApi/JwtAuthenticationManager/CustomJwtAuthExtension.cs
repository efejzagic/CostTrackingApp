using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JwtAuthenticationManager;
using Microsoft.OpenApi.Models;
using System.Reflection;
using DotNetEnv;
using Microsoft.IdentityModel.Logging;

namespace JwtAuthenticationManager
{
    public static class CustomJwtAuthExtension
    {

        public static void AddCustomJwtAuthentication(this IServiceCollection services)
        { 

            var realm = Environment.GetEnvironmentVariable("realm");
            var clientId = Environment.GetEnvironmentVariable("clientId");
            var clientSecret = Environment.GetEnvironmentVariable("clientSecret");
            var keycloakUrl = Environment.GetEnvironmentVariable("keycloakUrl");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
          IdentityModelEventSource.ShowPII = true; //Add this line
          options.Authority = $"{keycloakUrl}/realms/{realm}";
          //options.Authority = "https://lemur-10.cloud-iam.com/auth/realms/df-app";
          options.Audience = clientId; // The audience to validate against


          // Add the token validation parameters
          options.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuer = true,
              ValidateAudience = false,
              ValidateLifetime = true,
              ValidateIssuerSigningKey = true,
              ValidIssuer = $"{keycloakUrl}/realms/{realm}",
                ValidAudience = clientId,
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clientSecret)),
              SaveSigninToken = true,
          };
      });

        }
    }
}
