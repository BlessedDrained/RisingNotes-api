namespace MainLib.Extensions;

public static class ImageExtensions
{
    public static byte[] ToArray(this Image image)
    {
        using var ms = new MemoryStream();
        image.SaveAsync(ms, image.Metadata.DecodedImageFormat!);

        return ms.ToArray();
    }
}