using Auth.Application;
using Auth.WebAPI.Services;
using JwtAuthenticationManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<UserService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("O6qyJVLColeu3KnncWrk7NpTyDSvNJZN"))
          };
      });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthService API", Version = "v1" });

    // Add JWT authorization header
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
});

builder.Services.AddHttpClient();

//builder.Services.AddCustomJwtAuthentication();

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Contact = new OpenApiContact
//        {
//            Name = "Emil Fejzagic",
//            Email = "efejzagic2@etf.unsa.ba",
//        },
//        Version = "v1",
//        Title = "Cost Tracking API"
//    });
//});

builder.Services.AddSingleton<JwtTokenHandler>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(MediatorClass).GetTypeInfo().Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        // Add any other Swagger UI configuration as needed
    });

}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
