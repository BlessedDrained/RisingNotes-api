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
                    ValidAudience = "Api",
                    ValidIssuer = "http://localhost:5095",
                    IssuerSigningKey = JsonWebKey.Create(File.ReadAllText(jwtKeyFileName)),
                };
                config.RequireHttpsMetadata = false;
            });

        services.AddAuthorization(config =>
        {
            config.AddPolicy(PolicyConstant.RequireAtLeastUser,
                cfg =>
                    cfg.RequireRole(RoleConstants.User, RoleConstants.Author, RoleConstants.Admin));

            config.AddPolicy(PolicyConstant.RequireAtLeastAuthor, cfg => 
                cfg.RequireRole(RoleConstants.Author, RoleConstants.Admin).RequireClaim("authorId"));

            config.AddPolicy(PolicyConstant.RequireAdmin, cfg => 
                cfg.RequireRole(RoleConstants.Admin).RequireRole("authorId"));
        });

        return services;
    }
}