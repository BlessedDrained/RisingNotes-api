using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MainLib.Api.Validation;

/// <summary>
/// Атрибут валидации, что файл непустой
/// </summary>
public class NotEmptyFileAttribute : ValidationAttribute
{
    /// <inheritdoc/>
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true;
        }

        if (value is not IFormFile file)
        {
            return false;
        }

        return file.Length > 0;
    }
    
    /// <inheritdoc/>
    public override string FormatErrorMessage(string name)
    {
        return "File must not be empty";
    }
}