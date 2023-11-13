using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MainLib.Api.Validation;

/// <summary>
/// Атрибут, который проверяет, что все элементы перечисления - GUID
/// </summary>
public class AllEnumerableValuesAreGuidStringsAttribute : ValidationAttribute
{
    /// <inhertidoc/>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        if (value is not IEnumerable enumerableValue)
        {
            var propertyName = validationContext.MemberName;
            return new ValidationResult($"Field {propertyName} must implement IEnumerable interface");
        }
        
        var enumerator = enumerableValue.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;

            if (current is string currentString)
            {
                if (!Guid.TryParse(currentString, out var guid))
                {
                    return new ValidationResult($"Value {currentString} is not a valid GUID");
                }
            }
        }

        return ValidationResult.Success;
    }
}