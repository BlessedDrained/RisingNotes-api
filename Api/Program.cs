using Api;
using Dal;
using Logic;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder();
builder.Configuration.AddEnvironmentVariables();

if (builder.Environment.IsProduction())
{
    var port = int.TryParse(Environment.GetEnvironmentVariable("PORT"), out var parsedPort)
        ? parsedPort
        : 80;

    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

var lc = new LoggerConfiguration()
    .Enrich.WithThreadId()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);
if (builder.Environment.IsDevelopment())
{
    lc.WriteTo.Console(LogEventLevel.Debug, outputTemplate: "{Timestamp:HH:mm:ss:ms} LEVEL: [{Level}] THREAD: |{ThreadId}| {Message}{NewLine}{Exception}");
}
else
{
    lc.WriteTo.Seq("http://seq:5341", restrictedToMinimumLevel: LogEventLevel.Debug);
    lc.WriteTo.Console(LogEventLevel.Debug, outputTemplate: "{Timestamp:HH:mm:ss:ms} LEVEL: [{Level}] THREAD: |{ThreadId}| {Message}{NewLine}{Exception}");
}

Log.Logger = lc.CreateLogger();

builder.Services.AddSerilog((sp, loggerConfiguration) =>
{
    loggerConfiguration.Enrich.WithThreadId()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);

    if (builder.Environment.IsDevelopment())
    {
        loggerConfiguration.WriteTo.Console(LogEventLevel.Debug, outputTemplate: "{Timestamp:HH:mm:ss:ms} LEVEL: [{Level}] THREAD: |{ThreadId}| {Message}{NewLine}{Exception}");
    }
    else
    {
        loggerConfiguration.WriteTo.Seq("http://seq:5341", restrictedToMinimumLevel: LogEventLevel.Debug);
        loggerConfiguration.WriteTo.Console(LogEventLevel.Debug, outputTemplate: "{Timestamp:HH:mm:ss:ms} LEVEL: [{Level}] THREAD: |{ThreadId}| {Message}{NewLine}{Exception}");
    }
});

builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddLogicServices(builder.Configuration);
builder.Services.AddDalServices(builder.Configuration);

var app = builder.Build();

app.AddMiddlewares();

app.Run();