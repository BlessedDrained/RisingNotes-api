using System.Security.Claims;
using MainLib.Api.Auth.Constant;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MainLib.Api.Auth.Jwt;

public static class Startup
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        const string jwtKeyFileName = "tempkey.jwk";

        services
            .AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(config =>
            {
                config.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RoleClaimType = ClaimTypes.Role,
                    NameClaimType = ClaimTypes.Name,
                    // Audience = ApiScope
                    ValidAudience = "Api",
                    ValidIssuer = "http://localhost:5095",
                    IssuerSigningKey = JsonWebKey.Create(File.ReadAllText(jwtKeyFileName)),
                };
                // config.Events = new JwtBearerEvents()
                // {
                //     OnMessageReceived = context =>
                //     {
                //         if (!context.Request.Headers.TryGetValue("Authorization2", out var value))
                //         {
                //             context.NoResult();
                //             return Task.CompletedTask;
                //         }
                //
                //         var strValue = value.ToString();
                //
                //         if (strValue.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                //         {
                //             context.Token = strValue.Substring("Bearer ".Length).Trim();
                //         }
                //
                //         if (string.IsNullOrWhiteSpace(context.Token))
                //         {
                //             context.NoResult();
                //             return Task.CompletedTask;
                //         }
                //     
                //         return Task.CompletedTask;
                //     }
                // };
                config.RequireHttpsMetadata = false;
            });

        services.AddAuthorization(config =>
        {
            config.AddPolicy(PolicyConstant.RequireAtLeastUser,
                config =>
                    config.RequireRole(RoleConstants.User, RoleConstants.Author, RoleConstants.Admin));

            config.AddPolicy(PolicyConstant.RequireAtLeastAuthor, config => 
                config.RequireRole(RoleConstants.Author, RoleConstants.Admin));

            config.AddPolicy(PolicyConstant.RequireAdmin, config => 
                config.RequireRole(RoleConstants.Admin));
        });

        return services;
    }
}