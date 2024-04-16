using MainLib.CustomException;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;

/// <summary>
/// Переданный размер для resize некорректен
/// </summary>
public class InvalidImageSizeException : BadRequestException
{
    public InvalidImageSizeException() 
        : base(
            "Requested image dimensions are both equal zero. Provide non-zero value at least for one dimension.", RisingNotesErrorConstants.InvalidImageSize)
    {
    }
}