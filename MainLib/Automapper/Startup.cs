using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace MainLib.Automapper;

public static class Startup
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var config = new MapperConfiguration(x =>
        {
            x.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
            x.ShouldMapMethod = _ => false;
        });
        config.AssertConfigurationIsValid();

        var mapper = config.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}