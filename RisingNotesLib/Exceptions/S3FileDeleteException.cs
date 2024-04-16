using MainLib.CustomException;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;

/// <summary>
/// Ошибка во время удаления файла
/// </summary>
public class S3FileDeleteException : BadRequestException
{
    public S3FileDeleteException(string message) : base(message, RisingNotesErrorConstants.FileDelete)
    {
    }
}