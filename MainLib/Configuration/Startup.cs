using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MainLib.Configuration;

public static class Startup
{
    private const string EnvVariableName = "ASPNETCORE_ENVIRONMENT";
    private const string DevelopmentEnvironment = "Development";
    private const string ProducationEnvironment = "Production";
    
    
    
    public static IServiceCollection ConfigureConfigurationSources(this IServiceCollection services)
    {
        var builder = new ConfigurationBuilder();

        if (Environment.GetEnvironmentVariable(EnvVariableName) == DevelopmentEnvironment)
        {
            builder.AddJsonFile("appsettings.Development.json");
        }
        else if(Environment.GetEnvironmentVariable(EnvVariableName) == ProducationEnvironment)
        {
            builder.AddJsonFile("appsettings.json");
        }

        builder.AddEnvironmentVariables();

        var root = builder.Build();
        
        
        
        return services;
    }
}