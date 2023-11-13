using Microsoft.AspNetCore.Http;

namespace MainLib.Extensions;

/// <summary>
/// Расширения для <see cref="IFormFile"/>
/// </summary>
public static class FormFileExtensions
{
    /// <summary>
    /// Получить содержимое файла
    /// </summary>
    public static byte[] GetFileContent(this IFormFile file)
    {
        var stream = new MemoryStream();
        file.CopyTo(stream);
        return stream.ToArray();
    }
}