using System.ComponentModel.DataAnnotations;

namespace MainLib.Api.Validation;

/// <summary>
/// Атрибут, который сранивает значения двух полей
/// </summary>
public class GreaterOrEqualIntAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    /// <summary>
    /// Конструктор
    /// </summary>
    public GreaterOrEqualIntAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    /// <inheritdoc/>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        var currentValue = (int) value;

        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

        if (property == null)
        {
            throw new ArgumentException($"Property with name {_comparisonProperty} is not found");
        }

        var comparisonValue = (int) property.GetValue(validationContext.ObjectInstance);

        if (currentValue < comparisonValue)
        {
            var propertyName = validationContext.MemberName;
            return new ValidationResult($"Value = {currentValue} or field {propertyName} is less than value of field {_comparisonProperty} with value = {comparisonValue}");
        }

        return ValidationResult.Success;
    }
}