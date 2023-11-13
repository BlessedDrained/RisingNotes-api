using System.Text.Json;
using MainLib.CustomException;
using Microsoft.AspNetCore.Identity;

namespace RisingNotesLib.Exceptions;

public class UserRegistrationException : BadRequestException
{
    public UserRegistrationException(IEnumerable<IdentityError> errorList) : 
        base(JsonSerializer.Serialize(errorList))
    {
    }
}