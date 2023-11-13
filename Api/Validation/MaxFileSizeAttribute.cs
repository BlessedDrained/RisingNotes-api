using System.ComponentModel.DataAnnotations;
using Api.Controllers.File.Dto.Request;

namespace Api.Validation;

/// <summary>
/// Атрибут валидации, что файл меньше указанного размера
/// </summary>
public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxSizeKb;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="maxSizeKb">Максимальный размер файла в килобайтах</param>
    public MaxFileSizeAttribute(int maxSizeKb)
    {
        _maxSizeKb = maxSizeKb * 1024;
    }

    /// <inheritdoc/>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        if (value is not UploadFileRequest file)
        {
            return new ValidationResult("Field must represent CreateFileRequest entity");
        }

        if (file.File == null)
        {
            return new ValidationResult($"Field {nameof(file.File)} must not be null");
        }

        if (file.File.Length <= _maxSizeKb)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult($"File size must not be greater than {_maxSizeKb / 1024} KBytes");
    }
}