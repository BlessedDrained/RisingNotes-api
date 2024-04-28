using MainLib.CustomException;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;


public class UserNameIsAlreadyTakenException : BadRequestException
{
    public UserNameIsAlreadyTakenException() : base("This username is already taken")
    {
    }

    public UserNameIsAlreadyTakenException(string userName) : base($"Username '{userName}' is already taken", RisingNotesErrorConstants.UserNameIsAlreadyTaken)
    {
    }
}