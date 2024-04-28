using System.Text.Json;
using MainLib.CustomException;
using Microsoft.AspNetCore.Identity;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;

/// <inheritdoc cref="RisingNotesErrorConstants.AddToRole"/>
public class AddToRoleException : BadRequestException
{
    public AddToRoleException(IEnumerable<IdentityError> errorList) 
        : base(JsonSerializer.Serialize(errorList), RisingNotesErrorConstants.AddToRole)
    {
        
    }
}