using System.Text.Json;
using MainLib.CustomException;
using Microsoft.AspNetCore.Identity;

namespace RisingNotesLib.Exceptions;

public class AddToRoleException : BadRequestException
{
    public AddToRoleException(IEnumerable<IdentityError> errorList) :
        base(JsonSerializer.Serialize(errorList))
    {
    }
}