using MainLib.CustomException;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;

/// <summary>
/// У пользователя нет логотипа
/// </summary>
public class UserHasNoLogoException : BadRequestException
{
    public UserHasNoLogoException(Guid userId) 
        : base($"User with id={userId} has no logo", RisingNotesErrorConstants.UserHasNoLogo)
    {
    }
}