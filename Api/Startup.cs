using System.Text.Json;
using Api.IdentityServer;
using Api.Premanager;
using Dal.Context;
using MainLib.Api.Auth.Jwt;
using MainLib.Api.Route;
using MainLib.Api.Swagger;
using MainLib.Automapper;
using MainLib.ExceptionHandler;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Identity;
using RisingNotesLib.Models;
using Serilog;
using SixLabors.ImageSharp.Web.DependencyInjection;

namespace Api;

public static class Startup
{
    /// <summary>
    /// Добавить апи сервисы
    /// </summary>
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RouteOptions>(config =>
        {
            config.LowercaseUrls = true;
        });

        services.AddControllers(config =>
        {
            config.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            config.UseGeneralRoutePrefix("api");
            // config.UseGeneralRoutePrefix("api/v{version:apiVersion}");
        });

        services.AddSwaggerServices();

        services.AddJwtAuthentication();

        services.AddHttpClient();

        services.AddAutoMapper();

        services.AddCors(config =>
        {
            config.AddPolicy("AllowAll",
                policyBuilder =>
                {
                    policyBuilder
                        .AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowAnyMethod();
                });
        });

        services.AddPremanagers();

        services
            .AddImageSharp();
        // .InsertProvider<CustomImageProvider>(0);

        services.AddIdentity<AppIdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireLowercase = false;
                config.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

        services.AddIdentityServerService();

        services.Configure<JsonSerializerOptions>(x =>
        {
            // чтобы не пришлось писать JsonPropertyName
            x.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        // services.AddSerilogLogging();

        return services;
    }

    /// <summary>
    /// Добавить миддлвейры
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication AddMiddlewares(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        app.UseCustomExceptionHandler();

        app.UseCors("AllowAll");

        // app.UseImageSharp();

        app.UseIdentityServer();
        
        
        var swaggerEnabled = app.Configuration.GetValue<bool>("Swagger:IsEnabled");
        if (swaggerEnabled)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                // Чтобы можно было копировать ссылку на рест
                config.EnableTryItOutByDefault();
                config.EnableDeepLinking();
            });
        }

        app.MapControllers();

        return app;
    }
}