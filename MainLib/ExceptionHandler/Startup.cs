using MainLib.CustomException;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace MainLib.ExceptionHandler;

public static class Startup
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseExceptionHandler(config =>
        {
            config.Run(ProcessException);
        });

        return builder;
    }


    /// <summary>
    /// Получить модель ошибки
    /// </summary>
    private static Task ProcessException(HttpContext context)
    {
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        var error = feature.Error;
        var exceptionName = error.GetType().Name;

        // Если ошибка codeException, то это 400
        // Если нет, то надо смотреть на env
        // Если dev, то можно отдать стектрейс
        // Если нет, то нужно просто сказать что произошла ошибка

        if (error is BadRequestException codeException)
        {
            var model = new
            {
                Name = exceptionName,
                ErrorCode = codeException.ErrorCode,
                Description = codeException.Message,
                StackTrace = error.ToString()
            };

            context.Response.StatusCode = 400;
            context.Response.WriteAsJsonAsync(model);
        }
        else
        {
            // TODO сделать разделение на среды
            var model = new
            {
                Name = "InternalServerError",
                ErrorCode = 500,
                Description = "Error occured"
            };

            context.Response.WriteAsJsonAsync(model);
        }

        return Task.CompletedTask;
    }
}