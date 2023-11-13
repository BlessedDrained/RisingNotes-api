using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace MainLib.Api.Validation;

/// <summary>
/// Атрибут валидации, что файл имеет определенные форматы
/// </summary>
public partial class CustomFileExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _fileFormatList;
    
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="fileFormats"></param>
    public CustomFileExtensionsAttribute(string fileFormats)
    {
        var fileFormatList = fileFormats
            .Split(",")
            .Select(x => MyRegex().Replace(x, ""))
            .ToArray();
        _fileFormatList = fileFormatList;
    }
    
    /// <inheritdoc/>
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true;
        }

        if (value is not IFormFile file)
        {
            return true;
        }

        return _fileFormatList.Contains(Path.GetExtension(file.FileName));
    }
    
    /// <inheritdoc/>
    public override string FormatErrorMessage(string name)
    {
        return $"File extension must be {string.Join(", ", _fileFormatList)}";
    }

    [GeneratedRegex("\\s+")]
    private static partial Regex MyRegex();
}