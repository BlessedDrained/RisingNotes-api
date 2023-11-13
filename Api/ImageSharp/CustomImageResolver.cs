using SixLabors.ImageSharp.Web;
using SixLabors.ImageSharp.Web.Resolvers;

namespace Api.ImageSharp;

/// <inheritdoc />
public class CustomImageResolver : IImageResolver
{
    private readonly Stream _fileStream;
    
    /// 
    public CustomImageResolver(Stream fileStream)
    {
        _fileStream = fileStream;
    }

    /// <inheritdoc />
    public Task<ImageMetadata> GetMetaDataAsync()
    {
        return Task.FromResult<ImageMetadata>(default);
    }

    /// <inheritdoc />
    public Task<Stream> OpenReadAsync()
    {
        return Task.FromResult(_fileStream);
    }
}