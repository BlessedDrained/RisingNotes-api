using IdentityServer4.Services;
using RisingNotesLib.Models;

namespace Api.IdentityServer;

public static class Startup
{
    public static IServiceCollection AddIdentityServerService(this IServiceCollection services)
    {
        services.AddIdentityServer(config =>
            {
                config.Events.RaiseErrorEvents = true;
                config.Events.RaiseFailureEvents = true;
                config.Events.RaiseInformationEvents = true;
                config.Events.RaiseSuccessEvents = true;
                config.IssuerUri = "http://localhost:5095";
            })
            .AddAspNetIdentity<AppIdentityUser>()
            .AddProfileService<ProfileService>()
            .AddInMemoryClients(IdentityServerConfiguration.Clients)
            .AddInMemoryApiResources(IdentityServerConfiguration.ApiResoures)
            .AddInMemoryApiScopes(IdentityServerConfiguration.ApiScopes)
            .AddInMemoryIdentityResources(IdentityServerConfiguration.IdentityResources)
            .AddInMemoryPersistedGrants()
            .AddInMemoryCaching()
            .AddDeveloperSigningCredential();
        
        services.AddSingleton<ICorsPolicyService>((container) => {
            var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
            return new DefaultCorsPolicyService(logger)
            {
                AllowedOrigins = { "http://localhost:3000" }
            };
        });

        return services;
    }
}