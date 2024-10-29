using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MainLib.Api.Validation;

/// <summary>
/// Атрибут для проверки, что размер переданного файла равен как минимум переданному значению
/// </summary>
public class MinFileSizeAttribute : ValidationAttribute
{
    private readonly int _minFileSize;

    public MinFileSizeAttribute(int minFileSize)
    {
        _minFileSize = minFileSize;
    }
    
    public override bool IsValid(object value)
    {
        if (value == null)
        {
            return true;
        }

        if (value is IFormFile formFile && formFile.Length >= _minFileSize)
        {
            return true;
        }

        return false;
    }
}