using MainLib.CustomException;

namespace RisingNotesLib.Exceptions;

/// <summary>
/// Файл слишком большой, чтобы его можно было загрузить за один раз или он имеет несколько частей
/// </summary>
public class FileIsTooLargeToBeLoadedInOneOperationException : BadRequestException
{
    public FileIsTooLargeToBeLoadedInOneOperationException(string message) : base(message)
    {
    }

    public FileIsTooLargeToBeLoadedInOneOperationException(string message, int errorCode) : base(message, errorCode)
    {
    }
}