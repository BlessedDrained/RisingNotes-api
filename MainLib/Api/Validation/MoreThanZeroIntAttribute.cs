using System.ComponentModel.DataAnnotations;

namespace MainLib.Api.Validation;

/// <summary>
/// Атрибут валидации, который проверяет, что число больше нуля
/// </summary>
public class MoreThanZeroIntAttribute : ValidationAttribute
{
    private object _value;
    
    /// <inheritdoc/>
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true;
        }

        if (value is int intValue)
        {
            return intValue > 0;
        }
        
        return false;
    }
    
    /// <inheritdoc/>
    public override string FormatErrorMessage(string name)
    {
        return $"Field {name} value is not an integer or represents an int value that less or equal 0";
    }
}