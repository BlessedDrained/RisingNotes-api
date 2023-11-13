using System.ComponentModel.DataAnnotations;
using MainLib.Api.Auth.Constant;

namespace Api.Validation;

/// <summary>
/// Проверка, что передана корректная роль
/// </summary>
public class CorrectRole : ValidationAttribute
{
    /// <inheritdoc />
    public override bool IsValid(object value)
    {
        if (value == null)
        {
            return true;
        }

        if (value is not string stringValue)
        {
            return false;
        }

        return RoleConstants.IsRoleCorrect(stringValue);
    }
}