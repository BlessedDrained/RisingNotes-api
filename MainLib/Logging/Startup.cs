using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace MainLib.Logging;

public static class Startup
{
    public static IServiceCollection AddSerilogLogging(this IServiceCollection services)
    {
        var logger = new LoggerConfiguration()
            .Enrich.WithThreadId()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .WriteTo.Console()
            .WriteTo.Seq("http://seq:5341", apiKey: "kd6bszH9dOiWuIys4qvj", period: TimeSpan.FromSeconds(60), queueSizeLimit: 1)
            .CreateLogger();
        
        Log.Logger = logger;

        return services;
    }
}