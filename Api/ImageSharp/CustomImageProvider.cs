using Dal.File;
using Logic.File;
using SixLabors.ImageSharp.Web.Providers;
using SixLabors.ImageSharp.Web.Resolvers;

namespace Api.ImageSharp;

/// <summary>
/// Image provider для изображений из разных источников
/// </summary>
public class CustomImageProvider : IImageProvider
{
    private readonly IServiceProvider _serviceProvider;

    public CustomImageProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public bool IsValidRequest(HttpContext context)
    {
        if (!context.Request.Path.HasValue)
        {
            return false;
        }

        var path = context.Request.Path.Value;

        var splitPath = path.Split('/');

        if (splitPath.Length == 0)
        {
            return false;
        }

        return Guid.TryParse(splitPath[^1], out _);
    }

    /// <inheritdoc />
    public async Task<IImageResolver> GetAsync(HttpContext context)
    {
        var path = context.Request.Path.Value;
        var guid = Guid.Parse(path.Split('/')[^1]);

        FileDal file;
        using (var scope = _serviceProvider.CreateScope())
        {
            var fileManager = scope.ServiceProvider.GetRequiredService<IFileManager>();
            file = await fileManager.DownloadAsync(guid);
        }

        if (file.Extension == null)
        {
            throw new Exception();
        }

        return new CustomImageResolver(new MemoryStream(file.Content));
    }

    /// <inheritdoc />
    public ProcessingBehavior ProcessingBehavior { get; } = ProcessingBehavior.CommandOnly;

    /// <inheritdoc />
    public Func<HttpContext, bool> Match { get; set; } = _ => true;
}