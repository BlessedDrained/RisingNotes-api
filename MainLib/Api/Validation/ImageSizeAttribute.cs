using System.ComponentModel.DataAnnotations;
using MainLib.Helpers;
using Microsoft.AspNetCore.Http;
using Size = System.Drawing.Size;

namespace MainLib.Api.Validation;

/// <summary>
/// Атрибут валидации, что размер изображения меньше или равен указанному
/// </summary>
public class ImageSizeAttribute : ValidationAttribute
{
    private readonly string[] _imageFileFormats = {".jpg", ".jpeg", ".png"};
    private readonly Size _size;

    /// <summary>
    /// Конструктор
    /// </summary>
    public ImageSizeAttribute(int width, int height)
    {
        _size = new Size(width, height);
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

        if (!_imageFileFormats.Contains(Path.GetExtension(file.FileName)))
        {
            return true;
        }

        var size = ImageHelper.GetDimensions(new BinaryReader(file.OpenReadStream()));

        return size.Width <= _size.Width && size.Height <= _size.Height;
    }
    
    /// <inheritdoc/>
    public override string FormatErrorMessage(string name)
    {
        return $"Image size must be less or equal than {_size.Width} x {_size.Height}";
    }
}