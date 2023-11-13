using Microsoft.AspNetCore.StaticFiles;

namespace RisingNotesLib.Helper;

public static class ContentTypeHelper
{
    private static readonly FileExtensionContentTypeProvider _contentTypeProvider = new();

    /// <summary>
    /// Получить content type по расширению файла
    /// </summary>
    /// <param name="fileExtension">Расширение файла с точкой</param>
    public static string GetContentTypeByFileExtension(string fileExtension)
    {
        if (_contentTypeProvider.TryGetContentType(fileExtension, out var contentType))
        {
            return contentType;
        }

        throw new ArgumentOutOfRangeException("Such file extension is not allowed");
    }
}