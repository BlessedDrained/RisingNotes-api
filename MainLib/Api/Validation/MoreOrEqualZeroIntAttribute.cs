using System.ComponentModel.DataAnnotations;

namespace MainLib.Api.Validation;

/// <summary>
/// Атрибут валидации, что целое число больше или равно нулю
/// </summary>
public class MoreOrEqualZeroIntAttribute : ValidationAttribute
{
    /// <inheritdoc/>
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true;
        }

        if (value is not int intValue)
        {
            return false;
        }

        return intValue >= 0;
    }
    
    /// <inheritdoc/>
    public override string FormatErrorMessage(string name)
    {
        return $"Field {name} value is not an integer or represents an int value that less than 0";
    }
}