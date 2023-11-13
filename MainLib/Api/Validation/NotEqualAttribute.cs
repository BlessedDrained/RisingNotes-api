using System.ComponentModel.DataAnnotations;

namespace MainLib.Api.Validation;


/// <summary>
/// Атрибут, который проверяет, что два обьекта не равны
/// </summary>
public class NotEqualAttribute : ValidationAttribute
{
    private readonly string _toCompareField;

    /// <summary>
    /// Конструктор
    /// </summary>
    public NotEqualAttribute(string toCompareField)
    {
        _toCompareField = toCompareField;
    }
    
    /// <inheritdoc/>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }
        
        var property = validationContext.ObjectType.GetProperty(_toCompareField);

        if (property == null)
        {
            throw new ArgumentException("Property with this name is not found");
        }

        var comparisonValue = property.GetValue(validationContext.ObjectInstance);

        if (value.Equals(comparisonValue))
        {
            return new ValidationResult($"Current field is equal to field {_toCompareField}");
        }

        return ValidationResult.Success;
    }
}