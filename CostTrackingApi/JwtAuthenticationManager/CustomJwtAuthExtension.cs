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
            var realm = "cost-tracking-app";
            var keycloakUrl = "https://lemur-5.cloud-iam.com/auth";
            var clientId = "cost-tracking-client";
            var clientSecret = "O6qyJVLColeu3KnncWrk7NpTyDSvNJZN";

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
          IdentityModelEventSource.ShowPII = true; 
          options.Authority = $"{keycloakUrl}/realms/{realm}";
          options.Audience = clientId; 


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
