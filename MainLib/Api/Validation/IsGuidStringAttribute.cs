using System.ComponentModel.DataAnnotations;

namespace MainLib.Api.Validation;

/// <summary>
/// Атрибут валидации, что строка является GUID
/// </summary>
public class IsGuidStringAttribute : ValidationAttribute
{
    private object _value;
    
    /// <inheritdoc />
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true;
        }

        _value = value;

        if (value is not string stringValue)
        {
            return false;
        }

        return Guid.TryParse(stringValue, out var guid);
    }
    
    /// <inheritdoc/>
    public override string FormatErrorMessage(string name)
    {
        return $"Field {name} with value = {_value} is not a valid GUID";
    }
}