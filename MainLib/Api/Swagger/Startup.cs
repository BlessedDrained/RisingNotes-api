using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MainLib.Api.Swagger;

public static class Startup
{
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "RisingNotesApi",
                    Version = "v1",
                    Description = "API для стримингового сервиса RisingNotes"
                });
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            var xmlFilename = $"{Assembly.GetEntryAssembly()!.GetName().Name}.xml";
            var filePath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
            c.IncludeXmlComments(filePath);
            c.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization2",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
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
                    new string[] { }
                }
            });
        });

        return services;
    }
}