using Dal.File;
using MainLib.Extensions;
using MainLib.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Logic.Logo;

/// <inheritdoc />
public class LogoResizeService : ILogoResizeService
{
    /// <inheritdoc />
    public async Task<byte[]> ResizeAsync(FileDal file, int? width, int? height)
    {
        using var log = new MethodLog(file, width, height);
        
        using var image = await Image.LoadAsync(new MemoryStream(file.Content));
        image.Mutate(op => op.Resize(width.GetValueOrDefault(0), height.GetValueOrDefault(0)));

        var result = image.ToArray();
        
        log.ReturnsValue(result);
        return result;
        // if (width.HasValue && height.HasValue)
        // {
        //     op.Resize(width.Value, height.Value);
        // }
        // else if (width.HasValue)
        // {
        //     var curSize = op.GetCurrentSize();
        //     var newHeight = Convert.ToInt32(curSize.Height * (width.Value * 1.0 / curSize.Height));
        //     op.Resize(width.Value, newHeight);
        // }
        // else
        // {
        //     var curSize = op.GetCurrentSize();
        //     var newWidth = Convert.ToInt32(curSize.Width * (height.Value * 1.0 / curSize.Height));
        //     op.Resize(newWidth, height.Value);
    }
}