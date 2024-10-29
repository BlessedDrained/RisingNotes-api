using MainLib.CustomException;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;

public class ClipDoesNotBelongToCurrentAuthorException : BadRequestException
{
    public ClipDoesNotBelongToCurrentAuthorException(string message) : base(message, RisingNotesErrorConstants.ClipDoesNotBelongToCurrentAuthor)
    {
    }

    public ClipDoesNotBelongToCurrentAuthorException(Guid authorId, Guid clipId) 
        : base($"Clip with id={clipId} does not belong to author with id={authorId}", RisingNotesErrorConstants.ClipDoesNotBelongToCurrentAuthor)
    {
    }
}