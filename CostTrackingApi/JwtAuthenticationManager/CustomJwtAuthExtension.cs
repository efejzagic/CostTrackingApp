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
namespace JwtAuthenticationManager
{
    public static  class CustomJwtAuthExtension
    {

        public static void AddCustomJwtAuthentication(this IServiceCollection services)
        {
            //services.AddAuthentication(/*o =>
            //{
            //    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //}*/
            //    JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            //{
            //    o.Authority = "https://lemur-5.cloud-iam.com/auth/realms/cost-tracking-app";
            //    o.Audience = "cost-tracking-client";
            //    //o.RequireHttpsMetadata = false;
            //    //o.SaveToken = true;
            //    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //    {
            //        ValidateIssuer = false, //inace true
            //        ValidateAudience = false,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = "https://lemur-5.cloud-iam.com/auth/realms/cost-tracking-app",
            //        ValidAudience = "cost-tracking-client",
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("O6qyJVLColeu3KnncWrk7NpTyDSvNJZN"))
            //    };
            //});


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
          options.Authority = "https://lemur-5.cloud-iam.com/auth/realms/cost-tracking-app";
          options.Audience = "cost-tracking-client"; // The audience to validate against

          // Add the token validation parameters
          options.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuer = true,
              ValidateAudience = false,
              ValidateLifetime = true,
              ValidateIssuerSigningKey = true,
              ValidIssuer = "https://lemur-5.cloud-iam.com/auth/realms/cost-tracking-app",
              ValidAudience = "cost-tracking-client",
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("O6qyJVLColeu3KnncWrk7NpTyDSvNJZN")),
              SaveSigninToken = true,
          };
      });


        }
    }
}
